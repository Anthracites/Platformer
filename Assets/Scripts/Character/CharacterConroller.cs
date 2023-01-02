using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using Doozy.Engine;
using Zenject;
using Platformer.UIConnection;
using Platformer.Services;

namespace Platformer.GamePlay
{ 
public class CharacterConroller : MonoBehaviour
{
        [Inject]
        UI_Manager _uiManager;
        [Inject]
        GamePlay_Manager _gamePlayManager;

        [SerializeField]
        private int HP, jumpCount, frameCount = 0; // ХП перса
        [SerializeField]
        private float Speed = 1, Size, jumpforce, JumpHight; // скорость передвижения, размер, сила прыжка   

        [SerializeField]
        private bool IsFly = false; // полет
        [SerializeField]
        private bool IsNeuyas = false; // неуязвимость

        [SerializeField]
        private GameObject GamePad;
        [SerializeField]
        private MobileGamePad MobileContr;
        [SerializeField]
        private Rigidbody2D rb;
        [SerializeField]
        private Vector3 jmp, _startPoint;
        [SerializeField]
        private bool IsStaeyd = false;
        [SerializeField]
        private Button JumpButton;
        [SerializeField]
        private float t;
        [SerializeField]
        private Animator Anim;
        [SerializeField]
        private IEnumerator _getJumpHight, _moveAWSD;
        [SerializeField]
        private float _r0, _r1, _r2;
        [SerializeField]
        private LayerMask groundLayer;
        [SerializeField]
        private float collisionBuffer = 2, stepHeight, _distance, _stepOverHight;
        [SerializeField]
        private GameObject _pointPref;

        private float characterCenter, characterBottom, maxCollisionPointY, characterSize;
        private float[] contactPoints;
        private Vector3 relativePoint;

        void Start()
    {
            _uiManager.CharacterHP = HP;
            SetUIElements();
            jumpCount = 0;
        rb = GetComponent<Rigidbody2D>();
        jmp = transform.up * jumpforce;

        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
        JumpButton.onClick.AddListener(Jump);
        Anim = GetComponent<Animator>();
            _getJumpHight = GetJumpHight();
            _startPoint = gameObject.transform.position;
        }

       void SetUIElements()
        {
            StopAllCoroutines();
            GamePad = _uiManager.GamePad;
           JumpButton = _uiManager.JumpButton;
           MobileContr = GamePad.GetComponent<MobileGamePad>();
            _moveAWSD = MoveAWSD();
            StartCoroutine(_moveAWSD);

        }

        private IEnumerator GetJumpHight()
        { 
            _r0 = gameObject.transform.position.y;
            _r1 = _r0;
            while (true)
            {
                _r2 = gameObject.transform.position.y;
                if (_r2 > _r1)
                {
                    _r1 = _r2;
                }
                JumpHight = _r1 - _r0;
                //Debug.Log("_r0 = " + _r0.ToString() + ", _r1 =" + _r1.ToString() + ", _r2 =" + _r2.ToString());
                yield return null;
            }
        }

        private IEnumerator MoveAWSD()
        {
            while (true)
            {
                Vector2 MoveVector = Vector2.zero;
                MoveVector.x = MobileContr.Horizontal() * Speed;
                transform.Translate(MoveVector * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }


        public void Jump()
    {
            StartCoroutine(_getJumpHight);
            if ((IsStaeyd == true) | (jumpCount <= 1))
        {
                rb.AddForce(jmp, ForceMode2D.Impulse);
                jumpCount++;
         }
            IsStaeyd = false;
        }



        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Border")
            {
                GetDamage();
            }
        }
        void StepOver(Collision2D _barrier)
        {

            if (_barrier.gameObject.tag == "Platform")
            {
                characterCenter = gameObject.transform.position.y;
                characterBottom = characterCenter - ((GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y)/2);
                characterSize = GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y;
                var _contacts = _barrier.contacts;

                int i = 0;
                contactPoints = new float[_contacts.Length + 1];

                float maxY = characterBottom;

                foreach (ContactPoint2D _contact in _contacts)
                {
                    contactPoints[i] =_contact.point.y;
                    if(maxY < _contact.point.y)
                    {
                        maxY = _contact.point.y;
                    }
                    i++;
                }

                maxCollisionPointY = Mathf.Abs(maxY);
                float _stepHight = Mathf.Abs((characterBottom - maxY) * 1.01f);

                if (_stepHight < characterSize)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + _stepHight, transform.position.z);
                }
            }

        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            IsStaeyd = true;
            if (collision.gameObject.tag == "Border")
            {
                GetDamage();
            }

            StepOver(collision);
            jumpCount = 0;
            StopCoroutine(_getJumpHight);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
           if (collision.gameObject.tag == "Border")
            {
                GetDamage();
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            IsStaeyd = false;
        }
        void OnCollisionStay2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Border")
            {
                GetDamage();
            }
        }

    public void GetDamage()
    {
        StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {
        if ((IsNeuyas == false) & (HP > 1))
        {
                HP--;
            t = 2f;
            IsNeuyas = true;
            GameEventMessage.SendEvent(EventsLibrary.ChacterGotDamage);
            Anim.enabled = true;
            Anim.Play("PersDamaged");
            yield return new WaitForSeconds(t);
            IsNeuyas = false;
            Anim.enabled = false;
        }
        else if ((IsNeuyas == false) & (HP == 1))
            {
                StopCoroutine(_moveAWSD);

                IsNeuyas = true;
                GameEventMessage.SendEvent(EventsLibrary.ChacterGotDamage);
                Anim.enabled = true;
                Anim.Play("PersDamaged");
                yield return new WaitForSeconds(t);
                GameEventMessage.SendEvent(EventsLibrary.GameEnded);
                _gamePlayManager.IsContunueLevelEnable = false;
                DestroySelf();

        }
    }
        public void StopMove()
        {
            StopCoroutine(_moveAWSD);
        }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        private void StepOver(float _barrierHight)
        {
                gameObject.transform.position = new Vector3(transform.position.x, (transform.position.y + _barrierHight), gameObject.transform.position.z);
        }

        
        public class Factory : PlaceholderFactory<string, CharacterConroller>
        {

        }

    }
}
