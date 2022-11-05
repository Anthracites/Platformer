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

        public float LevelTime; // Время, за которое был пройден уровень
        public int Damage; // Полученный персонажем урон
        public int Shoots; // Выстрела за уровень
        public float Accuracy; // Точность стрельбы
        public int Coins; // Собранные монетки (возможно, будет другой дроп)

        public void Initialize()
        {

        }
    }
}
