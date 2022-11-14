using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Platformer.UIConnection;
using Doozy.Engine;

namespace Platformer.UserInterface
{
    public class UIView_Play : MonoBehaviour
    {
        [Inject]
        UI_Manager _uiManager;

        [SerializeField]
        private GameObject _character, _chacterHPSlot, _hpPanel;
        [SerializeField]
        private GameObject[] _chacterHPPictures;

        [SerializeField]
        private Button _jumpButton, _fireButtonL, _fireButtonR, _pauseButton;
        [SerializeField]
        private GameObject _gamePad;
        [SerializeField]
        private Slider _progressSlider;
        [SerializeField]
        private GameObject _miniMap, _miniMapPanel;

        [SerializeField]
        private int HPCount=0;

        private void Start()
        {
            HPCount = 0;
            SetUIElements();
        }
        private void OnEnable()
        {
                SetUIElements();
                GetCharacter();
                ConfigHPShow();
        }
        public void PauseGame()
        {
            Time.timeScale = 0;
        }
        void ConfigHPShow()
        {
            foreach (GameObject hp in _chacterHPPictures)
            {
                Destroy(hp);
            }

            HPCount = 0;
            int _hp = _uiManager.CharacterHP;
            _chacterHPPictures = new GameObject[_hp];
            int i = 0;
            while ( i < _hp)
            {
                _chacterHPSlot = Resources.Load(PrefabsPathLibrary.Character_HP_Slot) as GameObject;
                GameObject _inst_obj = Instantiate(_chacterHPSlot);
                _inst_obj.transform.SetParent(_hpPanel.transform);
                _chacterHPPictures[i] = _inst_obj;
                i++;
            }
            //Destroy(_chacterHPPicture);
        }

        public void SetUIElements()
        {
            _uiManager.GamePad = _gamePad;
            _uiManager.MobileContr = _gamePad.GetComponent<MobileGamePad>();
            _uiManager.JumpButton = _jumpButton;
            _uiManager.FireButtonL = _fireButtonL;
            _uiManager.FireButtonR = _fireButtonR;
            _uiManager.MiniMapPanel = _miniMapPanel;
            _uiManager.MiniMap = _miniMap;
            GetStartEndPoints();
            Debug.Log("UI element is send");
        }

        public void GetCharacter()
        {
            if (_uiManager.Character != null)
            {
                _character = _uiManager.Character;
                StartCoroutine(ShowProgress());
                Debug.Log("Character got");
            }
        }

        public void StartShowProgress()
        {
            StartCoroutine(ShowProgress());
        }

        public void GetDamage()
        {
            StartCoroutine(LoseHP());
        }

        private IEnumerator LoseHP()
        {
            _chacterHPPictures[HPCount].transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(1);
            Destroy(_chacterHPPictures[HPCount].transform.GetChild(0).gameObject);
            HPCount++;
        }

        public void ReloadGame()
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private IEnumerator ShowProgress()
        {
            while (true)
            {
                _progressSlider.value = _character.transform.position.x;
                yield return new WaitForEndOfFrame();
            }
            
        }

        public void GetStartEndPoints() //Дополнительно подписать на событие о спавне уровня
        {
            float _startPoint = _uiManager.StartPoint;
            float _endPoint = _uiManager.EndPoint;

            _progressSlider.GetComponent<Slider>().minValue = _startPoint;
            _progressSlider.GetComponent<Slider>().maxValue = _endPoint;
        }

        void OnDisable()
        {
            StopTraceCharacter();
        }
        public void StopTraceCharacter()
        {
            StopAllCoroutines();
        }
    }
}


