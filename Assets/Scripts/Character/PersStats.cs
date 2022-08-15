using System.Collections; 
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PersStats : MonoBehaviour // движение персонажа на сцене
{
    public int HP = 3; // ХП перса
    public float Speed = 1; // скорость перса   
    public float Size; // размер 
    public float jumpforce = 10f; // сила прыжка
    public bool IsFly = false; // полет
    public bool IsNeuyas = false; // неуязвимость


    public Vector2 MoveVector;
    public GameObject GamePad;
    public int jumpCount;
    private MobileGamePad MobileContr;
    private Rigidbody2D rb;
    private Vector3 jmp;
    public bool IsStaeyd = false;
    public GameObject CanvasControlObj;
    public Button JumpButton;
    public float t;
    private Animator Anim;
    public float JumpHight;


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
        MoveVector = Vector2.zero;
        MoveVector.x = MobileContr.Horizontal() * Speed;
       // MoveVector.y = MobileContr.Vertical() * Speed;
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
        if ((IsNeuyas == false)&(HP > 0))
        {
            HP--;
            t = 2f;
            IsNeuyas = true;
            CanvasControlObj.GetComponent<CanvasControl>().StartLoseHP();
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
