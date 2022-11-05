using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Platformer.UIConnection;
using Doozy.Engine;

namespace Platformer.GamePlay
{
    public class Shooting : MonoBehaviour
    {
        [Inject]
        UI_Manager _uiManager;

        [SerializeField]
        private GameObject BullPref;
        private GameObject inst_obj;
        public Button FireButtonL;
        public Button FireButtonR;
        private Vector3 SpawnPos;
        private bool IsShoot = true;

        private void Start()
        {
            SetFireButton();
        }
        public void SetFireButton()
        {
            FireButtonL = _uiManager.FireButtonL;
            FireButtonR = _uiManager.FireButtonR;
            FireButtonR.onClick.AddListener(StartShootR);
            FireButtonL.onClick.AddListener(StartShootL);
        }

        public void StartShootR()
        {
            if (IsShoot == true)
            {
                StartCoroutine(ShootR());
            }
        }

        public void StartShootL()
        {
            if (IsShoot == true)
            {
                StartCoroutine(ShootL());
            }
        }


        private IEnumerator ShootR()
        {
            SpawnPos = gameObject.transform.position;
            inst_obj = Instantiate(BullPref, SpawnPos, Quaternion.identity);
            IsShoot = false;
            yield return new WaitForSeconds(0.5f);
            IsShoot = true;
            GameEventMessage.SendEvent(EventsLibrary.CharacterShoot);
        }

        private IEnumerator ShootL()
        {
            Quaternion SpawnRot = Quaternion.Euler(0, 0, 180);
            SpawnPos = gameObject.transform.position;
            inst_obj = Instantiate(BullPref, SpawnPos, SpawnRot);
            IsShoot = false;
            yield return new WaitForSeconds(0.5f);
            IsShoot = true;
            GameEventMessage.SendEvent(EventsLibrary.CharacterShoot);
        }
    }
}
