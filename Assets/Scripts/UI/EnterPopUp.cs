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

namespace Platformer.UserInterface
{
    public class EnterPopUp : MonoBehaviour
    {
        [Inject]
        PopUp_Manager _popUpManager;
        [Inject]
        GamePlay_Manager _gamePlayManager;


        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Button ButtonOk;
        [SerializeField]
        private UIPopup _thisPopUp;
        [SerializeField]
        private TMP_InputField _inpupField;

        public void ShowThisPopUp()
        {
            GetKind();
            _inpupField.text = "";
            switch (_popUpManager.Title)
            {
                case ("Enter seed"):
                    ButtonOk.onClick.AddListener(SetSeed);
                    break;

                default:
                    ButtonOk.onClick.AddListener(ClosePopUp);
                    break;
                    
            }
            _thisPopUp.Show();
        }


        void GetKind()
        {
            _title.text = _popUpManager.Title;
            _icon.sprite = _popUpManager.Icon;
        }


        void SetSeed()
        {
            if (_inpupField.text != "")
            {
                _gamePlayManager.Seed = Int32.Parse(_inpupField.text);
                _gamePlayManager.UseSeed = true;
                GameEventMessage.SendEvent(EventsLibrary.CallLevelCreate);
            }
            ClosePopUp();
        }

        void ClosePopUp()
        {
            _thisPopUp.Hide();
        }
    }
}
