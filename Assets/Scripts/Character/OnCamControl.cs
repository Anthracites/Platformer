using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCamControl : MonoBehaviour
{
    private string PersTag;
    public GameObject MinMapCam;

    void Start()
    {
        PersTag = "Pers";
        
    }

   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == PersTag)
        {
            MinMapCam.GetComponent<MinMapCamMove>().enabled = true;
        }
    }
}
