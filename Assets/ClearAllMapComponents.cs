using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearAllMapComponents : MonoBehaviour
{
    public Button button;
    public GameObject Map;
    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        foreach (GameObject bruh in GameObject.FindGameObjectsWithTag("Delete"))
        {
            if (bruh.GetComponent<Drag>())
            {
                GameObject.Destroy(bruh);
            }
        }
        foreach (GameObject bruh in GameObject.FindGameObjectsWithTag("Custom"))
        {
            if (bruh.GetComponent<Drag>())
            {
                GameObject.Destroy(bruh);
            }
        }
    }
}
