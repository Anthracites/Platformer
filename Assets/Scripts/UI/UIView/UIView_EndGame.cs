using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Platformer.GamePlay;
using TMPro;

namespace Platformer.UserInterface
{
    public class UIView_EndGame : MonoBehaviour
    {
        [Inject]
        GamePlay_Manager _gamePlayManager;

        [SerializeField]
        private GameObject _statisticValuePanel;
        [SerializeField]
        private GameObject _generalStatisticPanel;
        [SerializeField]
        private float _levelTime, _levelDamage, _levelCoin, _levelAccuracy;

        void CleanStatistic()// Подписать на кнопку "Continue"
        {
            _levelAccuracy = 0;
            _levelTime = 0;
            _levelDamage = 0;
            _levelCoin = 0;

            for (int k = _generalStatisticPanel.transform.childCount; k > 0; --k)
            {
                DestroyImmediate(_generalStatisticPanel.transform.GetChild(0).gameObject);
            }

        }

        void ApplyStats(string _statName, float _statValue, string _unit)
        {
            var _statisticValuePanel = Instantiate(Resources.Load(PrefabsPathLibrary.StatisticValuePanel) as GameObject);
            _statisticValuePanel.transform.SetParent(_generalStatisticPanel.transform);
            _statisticValuePanel.transform.GetChild(0).GetComponent<TMP_Text>().text = _statName;
            _statisticValuePanel.transform.GetChild(1).GetComponent<TMP_Text>().text = _statValue.ToString() + _unit;
        }

        void GetStatisticFromConteiner()
        {
            _levelTime = _gamePlayManager.LevelTime;
            _levelDamage = _gamePlayManager.Damage;
            _levelCoin = _gamePlayManager.Coins;
            _levelAccuracy = _gamePlayManager.Accuracy;
        }

        void ShowStatistic()
        {
            ApplyStats("Level time:", _levelTime, " s");
            ApplyStats("Got damage:", _levelDamage, "");
            ApplyStats("Collected coins:", _levelCoin, "");
            ApplyStats("Accuracy on level:", _levelAccuracy, " %");
        }

        private void OnEnable()
        {
            CleanStatistic();
            GetStatisticFromConteiner();
            ShowStatistic();
        }
    }
}
