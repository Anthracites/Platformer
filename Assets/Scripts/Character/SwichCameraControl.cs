using UnityEngine;
using Doozy.Engine;
using Zenject;
using Platformer.UIConnection;

namespace Platformer.GamePlay
{

    public class SwichCameraControl : MonoBehaviour// класс для тригера, который переключает отслеживание персонажа на миникарте
    {
        [Inject]
        UI_Manager _uiManager;

        private string PersTag = "Pers";

        private void Start()
        {
            SetTriggerScale();
        }

        void SetTriggerScale()
        {
            float _startLevelPoint = _uiManager.StartPoint;
            float _endLevelPoint = _uiManager.EndPoint;

            float _levelCenterX = (_endLevelPoint - _startLevelPoint) / 2;
            GameObject _miniMapCamera = _uiManager.MiniMapCamera;
            Vector3 _spawnPosition = new Vector3(_levelCenterX, 0, -1);

            float _triggerSize = gameObject.GetComponent<SpriteRenderer>().size.x; // 0,16

            transform.position = _spawnPosition;
            float _scaleX = (_levelCenterX - _miniMapCamera.transform.position.x) * (1 - _triggerSize)* 10;
            if (_scaleX > 0)
            {
                transform.localScale = new Vector3(_scaleX, 1000, 1);
            }
            else
            {
                Destroy(gameObject);
            }
            //Debug.LogError("Width: " + Screen.currentResolution.width.ToString() + "MinimapSize: " + _uiManager.MiniMapPanel.transform.position.x.ToString() + "Camera's position: " + _miniMapCamera.transform.position.x.ToString());
            Debug.Log("Start level:" + _startLevelPoint + ", End level:" + _endLevelPoint);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            SwichTrace(other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
                //_exitPoint = (int)other.transform.position.x;
            SwichTrace(other.gameObject);
        }

        void SwichTrace(GameObject _object)
        {
            if (_object.tag == PersTag)
            {
                GameEventMessage.SendEvent(EventsLibrary.SwichTraceOnMiniMap);
            }
        }

        public class Factory : PlaceholderFactory<string, SwichCameraControl>
        {

        }
    }
}
