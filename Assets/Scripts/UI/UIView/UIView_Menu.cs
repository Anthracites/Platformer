using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using Doozy.Engine.UI;
using Zenject;
using Platformer.GamePlay;
using UnityEngine.UI;
using Platformer.UserInterface.PopUp;

namespace Platformer.UserInterface
{
    public class UIView_Menu : MonoBehaviour
    {
        [Inject]
        PopUp_Manager _popup_Manager;
        [Inject]
        GamePlay_Manager _gamePlayManager;

        [SerializeField]
        public UIPopup _enterSeedPopUp;
        [SerializeField]
        private Button _continueButton;

        private void Start()
        {
            _enterSeedPopUp.Hide();
        }

        private void OnEnable()
        {
            _continueButton.interactable = _gamePlayManager.IsContunueLevelEnable;    
        }

        public void StartRandomGame()
        {
            _gamePlayManager.UseSeed = false;
            GameEventMessage.SendEvent(EventsLibrary.CallLevelCreate);
            Game();
        }

        public void GoToCreator()
        {
            GameEventMessage.SendEvent(EventsLibrary.GoToCreator);
            Game();
        }

        public void EnterSeed()
        {
            _popup_Manager.CurrentPopUpConfig = PopUpConfigLibrary.EnterSeedPopUpConfig;
            GameEventMessage.SendEvent(EventsLibrary.ShowPopUp);
        }
        public void Game()
        {
            Time.timeScale = 1;
        }
    }
}
