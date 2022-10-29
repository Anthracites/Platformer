using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using Doozy.Engine.UI;
using Zenject;
using Platformer.GamePlay;
using UnityEngine.UI;

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
            _popup_Manager.Title = PopUpsLibrary.EnterSeedTitle;
            _popup_Manager.Icon = Resources.Load<Sprite>(PopUpsLibrary.EnterSeedIconResource);
            GameEventMessage.SendEvent(EventsLibrary.PlaySeedGame);
        }
        public void Game()
        {
            Time.timeScale = 1;
        }
    }
}
