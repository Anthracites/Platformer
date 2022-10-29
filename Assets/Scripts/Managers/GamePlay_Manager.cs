using Zenject;
using UnityEngine;

namespace Platformer.GamePlay
{
    public class GamePlay_Manager : IInitializable
    {
        public int Seed;
        public int LevelNumber;
        public bool UseSeed;
        public bool IsContunueLevelEnable;

        public void Initialize()
        {

        }
    }
}
