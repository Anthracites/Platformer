using Zenject;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.UIConnection
{

    public class UI_Manager : IInitializable
    {
        public GameObject GamePad;
        public MobileGamePad MobileContr;
        public Button JumpButton;

        public void Initialize()
        {

        }
    }
}
