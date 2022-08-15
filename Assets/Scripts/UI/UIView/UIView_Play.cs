using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Platformer.UIConnection;

namespace Platformer.UserInterface
{
    public class UIView_Play : MonoBehaviour
    {
        [Inject]
        UI_Manager _uiManager;

        private GameObject[] ChacterHPPictures;

        [SerializeField]
        private Button _jumpButton;
        [SerializeField]
        private GameObject _gamePad;

        [SerializeField]
        private GameObject[] HPObj;
        private int i = 0;

        private void Start()
        {
            _uiManager.GamePad = _gamePad;
            _uiManager.MobileContr = _gamePad.GetComponent<MobileGamePad>();
            _uiManager.JumpButton = _jumpButton;
        }
        public void GetDamage()
        {
            StartCoroutine(LoseHP());
        }

        private IEnumerator LoseHP()
        {
            ChacterHPPictures[i].GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(1);
            ChacterHPPictures[i].SetActive(false);
            i++;
        }

        public void ReloadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}


