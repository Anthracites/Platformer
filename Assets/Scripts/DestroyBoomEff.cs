using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoomEff : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyBorder());
    }

    private IEnumerator DestroyBorder()
    {
        yield return new WaitForSeconds(0.65f);
        Destroy(gameObject);
    }
}
