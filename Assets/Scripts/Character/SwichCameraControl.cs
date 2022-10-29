using UnityEngine;
using Doozy.Engine;
using Zenject;

namespace Platformer.GamePlay
{

    public class SwichCameraControl : MonoBehaviour// класс для тригера, который переключает отслеживание персонажа на миникарте
    {
        private string PersTag = "Pers";
        [SerializeField]
        private int _enterPoint, _exitPoint;


        void OnTriggerEnter2D(Collider2D other)
        {

            if (other.tag == PersTag)
            {
                _enterPoint = (int)other.transform.position.x;
            }
            //SwichTrace();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag == PersTag)
            {
                _exitPoint = (int)other.transform.position.x;
            }
            SwichTrace();
        }

        void SwichTrace()
        {
            if (_enterPoint != _exitPoint)
            {
                GameEventMessage.SendEvent(EventsLibrary.SwichTraceOnMiniMap);
            }
        }

        public class Factory : PlaceholderFactory<string, SwichCameraControl>
        {

        }
    }
}
