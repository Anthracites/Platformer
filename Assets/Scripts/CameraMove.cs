using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour // Движение камеры за персонажем
{
    public GameObject PersObj;

    void Update()
    {
        MoveWhithPerson();
    }

    void MoveWhithPerson()
    {
        gameObject.transform.position = new Vector3(PersObj.transform.position.x, PersObj.transform.position.y, gameObject.transform.position.z);
    }
}
