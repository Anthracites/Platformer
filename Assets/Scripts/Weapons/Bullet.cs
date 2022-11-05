using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;

namespace Platformer.GamePlay
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D rb;
        [SerializeField]
        private float bulletforce = 10f;
        private Vector3 bulletDes;
        [SerializeField]
        private GameObject BulletObj;
        [SerializeField]
        private GameObject BoomEffPref;
        [SerializeField]
        private GameObject inst_obj;

        void Start()
        {

            StartCoroutine(DestroyBullet());
            GetForsDest();
        }

        private IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(3.03f);
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {

                if (other.tag == "Border")
                {
                    other.GetComponent<BorderCS>().StartDestroy();
                GameEventMessage.SendEvent(EventsLibrary.CharacterHitTarget);
                Destroy(gameObject);
            }
            if (other.tag == "Platform")
            {
                Destroy(gameObject);
            }
        }
        void OnDestroy()
        {
            Vector3 SpawnPos = gameObject.transform.position;
            inst_obj = Instantiate(BoomEffPref, SpawnPos, Quaternion.identity);
        }

        void GetForsDest()
        {
            bulletDes = transform.right * bulletforce;
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(bulletDes, ForceMode2D.Impulse);
        }
    }
}
