using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject BullPref;
    private GameObject inst_obj;
    public Button FireButtonL;
    public Button FireButtonR;
    private Vector3 SpawnPos;
    private bool IsShoot = true;

    void Start()
    {
        FireButtonR.onClick.AddListener(StartShootR);
        FireButtonL.onClick.AddListener(StartShootL);
    }

    public void StartShootR()
    {
        if (IsShoot == true)
        {
            StartCoroutine(ShootR());
            Debug.Log("SootR");
        }
    }

    public void StartShootL()
    {
        if (IsShoot == true)
        {
            StartCoroutine(ShootL());
            Debug.Log("SootL");
        }
    }


    private IEnumerator ShootR()
    {
        SpawnPos = gameObject.transform.position;
        inst_obj = Instantiate(BullPref, SpawnPos, Quaternion.identity) as GameObject;
        inst_obj.GetComponent<Bullet>().k = 1;
        IsShoot = false;
        yield return new WaitForSeconds(0.5f);
        IsShoot = true;
    }

    private IEnumerator ShootL()
    {
        Quaternion SpawnRot = Quaternion.Euler(0, 0, 180);
        SpawnPos = gameObject.transform.position;
        inst_obj = Instantiate(BullPref, SpawnPos, SpawnRot) as GameObject;
        //inst_obj.GetComponent<Bullet>().k = -1f;
        IsShoot = false;
        yield return new WaitForSeconds(0.5f);
        IsShoot = true;
    }
}
