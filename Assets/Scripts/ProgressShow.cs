using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressShow : MonoBehaviour // Отображение прогресса прохождения уровня
{
    public GameObject PersObj;
    public float StartCoordX;
    public float EndCoordX;
    public float PersCoord;

    void Start()
    {
   
    }

    void OnEnable()
    {
        gameObject.GetComponent<Slider>().minValue = StartCoordX;
        gameObject.GetComponent<Slider>().maxValue = EndCoordX;
    }

    void Update()
    {
        gameObject.GetComponent<Slider>().value = PersObj.transform.position.x;

    }
}
