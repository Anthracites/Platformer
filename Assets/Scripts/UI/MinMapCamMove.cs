using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapCamMove : MonoBehaviour
{
    public GameObject PersObj;

    void  Start()
    {
        transform.LookAt(PersObj.transform.position);
    }

    void Update()
    {
        MoveWhithPerson();
    }

    void MoveWhithPerson()
    {
        transform.LookAt(new Vector3 (PersObj.transform.position.x, 3.7f, 0));
        //gameObject.transform.position = new Vector3(PersObj.transform.position.x + 45, gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
