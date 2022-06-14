using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitFunction : MonoBehaviour
{
    public Save Savey;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Wait());
        }
    }
    
    IEnumerator Wait()
    {
        Savey.StartCoroutine(Savey.SaveGame(true));
        yield return new WaitForSeconds(0.1f);
        while (Savey.Finished == false)
        {
            yield return new WaitForEndOfFrame();
        }
        Application.Quit(0);
    }
}
