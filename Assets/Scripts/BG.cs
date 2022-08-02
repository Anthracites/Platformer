using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    private Transform camTransform;
    private Vector3 lastCamPos;

    void Start()
    {
        camTransform = Camera.main.transform;
        lastCamPos = camTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMove = camTransform.position - lastCamPos;
        float parallax = .9f;
        camTransform.position += deltaMove * parallax;
        lastCamPos = camTransform.position;
    }
}
