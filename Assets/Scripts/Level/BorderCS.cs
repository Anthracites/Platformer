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
        private int j = 0;
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

        }

        public void CreateCollider()
        {
            Coll = gameObject.AddComponent<PolygonCollider2D>();
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
