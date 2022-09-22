using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine;
using Doozy.Engine.UI;
using Zenject;
using Platformer.GamePlay;

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

        private void Start()
        {
            _enterSeedPopUp.Hide();
        }

        public void StartRandomGame()
        {
            _gamePlayManager.UseSeed = false;
            GameEventMessage.SendEvent(EventsLibrary.CallLevelCreate);
        }

        public void EnterSeed()
        {
            _popup_Manager.Title = PopUpsLibrary.EnterSeedTitle;
            _popup_Manager.Icon = Resources.Load<Sprite>(PopUpsLibrary.EnterSeedIconResource);
            GameEventMessage.SendEvent(EventsLibrary.PlaySeedGame);
        }
    }
}
