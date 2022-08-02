using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatsSaverCS : MonoBehaviour
{
    public int GenSeed; //Сид
    public int HP; // ХэПэ перса
    public float Speed; //Скорость перса 
    public float Size; //Размер перса
    public float JumpForse; //Сила (высота) прыжка перса
    public bool IsFly; // Летает ли перс
    public bool IsNeuyas; //Неуязвимоть перса

    void SaveStats()
    {
        #region PlayerP refs.Set***
        PlayerPrefs.SetInt("GenSeed", GenSeed);
        PlayerPrefs.SetInt("HP", HP);

        PlayerPrefs.SetFloat("Speed", Speed);
        PlayerPrefs.SetFloat("Size", Size);
        PlayerPrefs.SetFloat("JumpForse", JumpForse);

        if (IsFly == true)
        {
            PlayerPrefs.SetString("PersFly", "PersFly");
        }
        else
        {
            PlayerPrefs.SetString("PersNOFly", "PersNOFly");
        }

        if (IsNeuyas == true)
        {
            PlayerPrefs.SetString("PersNeuyas", "PersNeuyas");
        }
        else
        {
            PlayerPrefs.SetString("PersNONeuyas", "PersNONeuyas");
        }
        #endregion
        Debug.Log("Settings uploaded to Windows Registry");
    }
}
