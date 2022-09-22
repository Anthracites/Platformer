using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Doozy.Engine;
using Zenject;
using Platformer.UIConnection;

namespace Platformer.GamePlay
{ 
public class CharacterConroller : MonoBehaviour
{
        [Inject]
        UI_Manager _uiManager;

        [SerializeField]
        private int HP, jumpCount; // ХП перса
        [SerializeField]
        private float Speed = 1, Size, jumpforce = 10f; // скорость перса   

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
        private Vector3 jmp;
        [SerializeField]
        private bool IsStaeyd = false;
        [SerializeField]
        private Button JumpButton;
        [SerializeField]
        private float t;
        [SerializeField]
        private Animator Anim;
        [SerializeField]
        private float JumpHight;
        [SerializeField]
        private Vector3 _startPoint;


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
            GetJumpHight();
            _startPoint = gameObject.transform.position;
    }

       void SetUIElements()
        {
            StopAllCoroutines();
            GamePad = _uiManager.GamePad;
           JumpButton = _uiManager.JumpButton;
           MobileContr = GamePad.GetComponent<MobileGamePad>();
            StartCoroutine(MoveAWSD());
        }


    void GetJumpHight()
    {

        float r = transform.position.y;
        if (r > JumpHight)
        {
            JumpHight = r;
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
        if ((IsStaeyd == true) & (jumpCount < 1))
        {
            rb.AddForce(jmp, ForceMode2D.Impulse);
            jumpCount++;
            IsStaeyd = false;
        }
        else
        {
            jumpCount = 0;
        }

    }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Border")
            {
                GetDamage();
            }
            Debug.Log("Collision with " + other.name);
        }

        void OnCollisionStay2D(Collision2D collision)
    {
        IsStaeyd = true;
        }

    public void GetDamage()
    {
        StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {
        if ((IsNeuyas == false) & (HP > 1))
        {
                Debug.Log("Pers damaged");
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
                Debug.Log("Game over!!!!");
                StopCoroutine(MoveAWSD());
                Anim.enabled = true;
                Anim.Play("PersDamaged");
                yield return new WaitForSeconds(t);
                GameEventMessage.SendEvent(EventsLibrary.GameEnded);
                DestroySelf();

        }
    }

        public void DestroySelf()
        {
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<string, CharacterConroller>
        {

        }

    }
}
