using System.Collections;
using UnityEngine;

public class AutoDestruccion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(AutoDestruction());
    }

    IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(3);
        if (gameObject != null)
            Destroy(gameObject);
    }
}
