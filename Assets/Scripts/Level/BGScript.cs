using UnityEngine;

namespace Platformer.GamePlay
{
    public class BGScript : MonoBehaviour
    {
        [SerializeField]
        private float startPos, length, parallaxEffect;
        [SerializeField]
        public GameObject _camera;

        void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        void Update()
        {
            float temp = _camera.transform.position.x * (1 - parallaxEffect);
            float dist = _camera.transform.position.x * parallaxEffect;

            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            if (temp > startPos + length)
                startPos += length;
            else if (temp < startPos - length)
                startPos -= length;
        }
    }
}
