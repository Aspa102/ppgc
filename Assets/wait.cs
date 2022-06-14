using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wait : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(eggs()); 
    }
    IEnumerator eggs()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
