using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Doozy.Engine;
using Platformer.UIConnection;

namespace Platformer.GamePlay
{
    public class LevelComplete : MonoBehaviour
    {
        [Inject]
        UI_Manager _uiManager;
        [Inject]
        GamePlay_Manager _gamePlay_Manager;

        [SerializeField]
        private GameObject _character;
        
        public void GetCharacter()
        {
            _character = _uiManager.Character;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == _character)
            {
                Debug.Log("Level complete");
                _gamePlay_Manager.IsContunueLevelEnable = false;
                GameEventMessage.SendEvent(EventsLibrary.GameEnded);
//                Debug.LogError("Trigger!!!");
            }
        }

        private void OnCollisionEnter2D(Collider2D collision)
        {
            if (collision.gameObject == _character)
            {
                Debug.Log("Level complete");
                _gamePlay_Manager.IsContunueLevelEnable = false;
                GameEventMessage.SendEvent(EventsLibrary.GameEnded);
                Debug.LogError("Collision!!!");
            }
        }
    }
}
