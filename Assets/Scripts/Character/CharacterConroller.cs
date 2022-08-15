using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Doozy.Engine;

namespace Platformer.GamePlay
{ 
public class CharacterConroller : MonoBehaviour
{
        [SerializeField]
        private int HP = 3; // ХП перса
        [SerializeField]
        private float Speed = 1; // скорость перса   
        [SerializeField]
        private float Size; // размер 
        [SerializeField]
        private float jumpforce = 10f; // сила прыжка
        [SerializeField]
        private bool IsFly = false; // полет
        [SerializeField]
        private bool IsNeuyas = false; // неуязвимость

        [SerializeField]
        private GameObject GamePad;
        [SerializeField]
        private int jumpCount;
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


    void Start()
    {
        jumpCount = 0;
        rb = GetComponent<Rigidbody2D>();
        jmp = transform.up * jumpforce;

        MobileContr = GamePad.GetComponent<MobileGamePad>();
        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
        JumpButton.onClick.AddListener(Jump);
        Anim = GetComponent<Animator>();
    }

    void GetJumpHight()
    {

        float r = transform.position.y;
        if (r > JumpHight)
        {
            JumpHight = r;
        }
    }

    void Update()
    {
        GetJumpHight();
        {
            PersMoveAWSD();
        }

    }

    void PersMoveAWSD()
    {
            Vector2 MoveVector = Vector2.zero;
            MoveVector.x = MobileContr.Horizontal() * Speed;
            transform.Translate(MoveVector * Time.deltaTime);

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
        if ((IsNeuyas == false) & (HP > 0))
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
        else if (HP == 0)
        {
            yield return new WaitForSeconds(t);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


}

}
