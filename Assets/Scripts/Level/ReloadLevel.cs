 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Platformer.GamePlay
{
    public class ReloadLevel : MonoBehaviour // Перезапуск уровня после падения
    {
        [Inject]
        UIConnection.UI_Manager _uiManager;

        [SerializeField]
        private GameObject _character;
        [SerializeField]
        private Vector2 _startCharacterPosition;
        [SerializeField]
        private bool IsTrigged = false;
        [SerializeField]
        private GameObject MinMapCam;

        public void GetCharacter()
        {
            _character = _uiManager.Character;
            _startCharacterPosition = _character.transform.position;

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            IsTrigged = true;

            if (other.gameObject == _character)
            {
                _character.transform.position = _startCharacterPosition;
                //MinMapCam.GetComponent<MinMapCamMove>().enabled = true;
            }
        }
    }
}
