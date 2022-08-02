using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCS: MonoBehaviour
{
    public PolygonCollider2D Coll;
    public int i = 0;
    public int j = 0;
    public GameObject Pers;
    private Animation anim;
    private Animator animContr;
    private bool IsDamaged = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animation>();
        animContr = gameObject.GetComponent<Animator>();

    }

    public void CreateCollider()
    {
        Coll =  gameObject.AddComponent<PolygonCollider2D>();
        i++;
    }

    public void ChangeColliderShape()
    {
        RemoveCollider();
        CreateCollider();
    }

    public void RemoveCollider()
    {
        Destroy(Coll);
        j++;
    }

    public void StartDestroy()
    {
        StartCoroutine(DestroyBorder());
    }

    private IEnumerator DestroyBorder()
    {
        animContr.Play("IcePillarAnimDestroy");
        float length = gameObject.GetComponent<Animation>().clip.length;
        yield return new WaitForSeconds(length);
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pers")
        {
            Pers.GetComponent<PersStats>().GetDamage();
        }
    }


}
/*
 public void NoEnabledCollyder()
    {
        BarierColl.enabled = false;
    }

    public void EnabledCollyder()
    {
        BarierColl.enabled = true;
    }
*/
