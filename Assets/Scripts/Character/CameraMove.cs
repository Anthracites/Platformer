using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Platformer.UIConnection;

namespace Platformer.GamePlay
{
    public class CameraMove : MonoBehaviour // Движение камеры за персонажем
    {
        [Inject]
        UI_Manager _uiManager;

        [SerializeField]
        private GameObject _character;

        public void TraceCharacter()
        {
            GetCharacter();
        }


        private IEnumerator MoveWhithCharacter()
        {
            while (_character != null)
            {
                gameObject.transform.position = new Vector3(_character.transform.position.x, _character.transform.position.y, gameObject.transform.position.z);
                yield return new WaitForEndOfFrame();
            }
        }

        public void GetCharacter()
        {
            _character = _uiManager.Character;
            StartCoroutine(MoveWhithCharacter());
        }

        public void StopTraicing()// Подписать на событие наступление паузы и окончания игры
        {
            StopCoroutine(MoveWhithCharacter());
        }
    }
}
