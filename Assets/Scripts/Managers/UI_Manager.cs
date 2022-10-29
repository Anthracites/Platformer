using Zenject;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Platformer.UIConnection
{

    public class UI_Manager : IInitializable
    {
        public GameObject Character;
        public GameObject GamePad;
        public GameObject MiniMapPanel;
        public Vector3 MiniMapCameraPosition;
        public MobileGamePad MobileContr;
        public Button JumpButton, FireButtonL, FireButtonR;
        public GameObject MiniMap;
        public int CharacterHP;

        public float StartPoint, EndPoint;

        public void Initialize()
        {

        }
    }
}
