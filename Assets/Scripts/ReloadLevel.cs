 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevel : MonoBehaviour // Перезапуск уровня после падения
{
    public bool IsTrigged = false;
    public GameObject MinMapCam;

    void OnTriggerEnter2D(Collider2D other)
    {
        IsTrigged = true;

        if (other.gameObject.tag == "Pers")
        {
           other.gameObject.transform. position = new Vector2(0f, 5f);
           MinMapCam.GetComponent<MinMapCamMove>().enabled = true;
        }
    }
}
