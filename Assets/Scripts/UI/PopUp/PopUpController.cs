using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using Doozy.Engine.UI;
using Doozy.Engine;
using Platformer.GamePlay;
using UniRx;
using Platformer.UserInterface.PopUp;
using UnityEngine.TextCore.Text;

namespace Platformer.UserInterface
{
    public class PopUpController : MonoBehaviour
    {
        [Inject]
        PopUp_Manager _popUpManager;
        [Inject]
        GamePlay_Manager _gamePlayManager;


        [SerializeField]
        private PopUpConfig _currentPopUpConfug;

        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Button ButtonOk, ButtonPaste, _cancelButton, _closeButton;
        [SerializeField]
        private GameObject _input;
        [SerializeField]
        private UIPopup _thisPopUp;
        [SerializeField]
        private TMP_InputField _inpupField;
        [SerializeField]
        private bool _isInoutActive;
        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private string _popUpName;

        public void ShowThisPopUp()
        {
            GetKind();
            _inpupField.text = "";
            _thisPopUp.Show();
        }


        void GetKind()
        {
            _currentPopUpConfug = _popUpManager.CurrentPopUpConfig;
            ConfigPopUp(_currentPopUpConfug);

            ButtonOk.onClick.RemoveAllListeners();
            switch (_popUpName)
            {
                case ("EnterSeedPopUp"):
                    ButtonOk.onClick.AddListener(SetSeed);
                    break;
                case ("SeedCopiedPopUp"):
                    ButtonOk.onClick.AddListener(ClosePopUp);
                    break;
                default:
                    ButtonOk.onClick.AddListener(ClosePopUp);
                    break;
            }
        }

        void ConfigPopUp(PopUpConfig _config)
        {
            _popUpName = _config.PopUpName;
            _title.text = _config.Title;
           _icon.sprite = Resources.Load<Sprite>(_config.IconWay);
            _closeButton.gameObject.SetActive(_config.IsActiveCloseButton);
            _input.SetActive(_config.IsActiveInputField);
            _cancelButton.gameObject.SetActive(_config.IsActiveCancelButton);
            _thisPopUp.HideOnClickOverlay = (_config.CloseAnywareClick);
            _thisPopUp.HideOnClickContainer = (_config.CloseAnywareClick);
        }

        void SetSeed()
        {
            if (_inpupField.text != "")
            {
                _gamePlayManager.Seed = Int32.Parse(_inpupField.text);
                _gamePlayManager.UseSeed = true;
                GameEventMessage.SendEvent(EventsLibrary.CallLevelCreate);
                Time.timeScale = 1;
            }
            ClosePopUp();
        }

        public void PasteFromBuffer()
        {
            TextEditor _text = new TextEditor();
            _text.Paste();
            _inpupField.text = _text.text;
        }

        public void ClosePopUp()
        {
            _thisPopUp.Hide();
        }
    }
}
