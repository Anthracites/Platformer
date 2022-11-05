using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Platformer.UIConnection;

namespace Platformer.GamePlay
{
    public class BorderCS : MonoBehaviour
    {
        [Inject]
        UI_Manager _uiManager;

        [SerializeField]
        private PolygonCollider2D Coll;
        [SerializeField]
        private int i = 0;
        [SerializeField]
        private GameObject _character;
        [SerializeField]
        private Animation anim;
        [SerializeField]
        private Animator animContr;
        [SerializeField]
        private bool IsDamaged = false;

        void Start()
        {
            _character = _uiManager.Character.gameObject;
            anim = gameObject.GetComponent<Animation>();
            animContr = gameObject.GetComponent<Animator>();
            Coll = gameObject.GetComponent<PolygonCollider2D>();
            ConfigPillar();
        }

        void ConfigPillar()
        {
            float s = Random.Range(0.25f, 0.5f);
            float f = Random.Range(0, 2);
            bool b = Mathf.Approximately(f,1);
            if (b)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            animContr.speed = s;
//            Debug.Log(f.ToString() + "," + b.ToString());
        }

        public void ChangeTag()
        {
            RemoveCollider();
            //gameObject.tag = "UnactiveBorder";
        }

        public void RemoveCollider()
        {
            Destroy(Coll);
        }

        public void StartDestroy()
        {
            StartCoroutine(DestroyBorder());
        }

        private IEnumerator DestroyBorder()
        {
            animContr.speed = 1;
            animContr.Play("IcePillarAnimDestroy");
            float length = gameObject.GetComponent<Animation>().clip.length;
            yield return new WaitForSeconds(length);
            Destroy(gameObject);
        }

        public class Factory : PlaceholderFactory<string, BorderCS>
        {

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
