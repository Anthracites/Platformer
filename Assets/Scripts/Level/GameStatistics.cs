using System.Collections;
using System;
using UnityEngine;
using Zenject;

namespace Platformer.GamePlay
{
    public class GameStatistics : MonoBehaviour
    {
        [Inject]
        GamePlay_Manager _gamePlayManager;

        [SerializeField]
        private float _levelTime, _accuracy;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private int _shoots;
        [SerializeField]
        private int _hitTargets;
        [SerializeField]
        private int _coins;
        [SerializeField]
        private IEnumerator _timeCounter;


        private void Start()
        {
            _timeCounter = TimeCount();
        }

        public void StartCollectStatictics() // Подписать на "LevelCreated"
        {
            ResetStatistic();
            StartCoroutine(_timeCounter);
        }

        public void DamageCount() // Перевесить на рективность
        {
            _damage++;
        }

        public void CointCount()//Перевестить на реактивность
        {
            _coins++;
        }
        public void ShootCount()
        {
            _shoots++;
        }

        public void HitTagetCount()
        {
            _hitTargets++;
        }

        private IEnumerator TimeCount()
        {
            while(true)
            {
                _levelTime++;
                yield return new WaitForSeconds(1);
            }
        }

        private void CountAccuracy()
        {
            _accuracy = (float)Math.Round(((float)_hitTargets / (float)_shoots), 2) * 100;
        }

        public void SendStatistics() // Подсписать на событие LevelComplete и GameEnded
        {
            CountAccuracy();
            StopCoroutine(_timeCounter);
            _gamePlayManager.LevelTime = _levelTime;
            _gamePlayManager.Damage = _damage;
            _gamePlayManager.Coins = _coins;
            _gamePlayManager.Shoots = _shoots;
            _gamePlayManager.Accuracy = _accuracy;
        }
        public void ResetStatistic()// Вызывать перед новым запуском
        {
            _levelTime = 0;
            _damage = 0;
            _coins = 0;
            _shoots = 0;
            _accuracy = 0;
            _hitTargets = 0;
        }
    }
}
