using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] HPObj;
    private int i = 0;

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartLoseHP()
    {
        StartCoroutine(LoseHP());
    }

    private IEnumerator LoseHP()
    {
        HPObj[i].GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1);
        HPObj[i].SetActive(false);
        i++;
    }
}
