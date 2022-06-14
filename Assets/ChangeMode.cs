using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMode : MonoBehaviour
{
    public Button button;
    public GameObject gui;
    public bool Mode = false;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !gui.activeInHierarchy)
        {
            if (!Mode)
            {
                button.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = "Scale";
                Mode = true;
            }
            else
            {
                button.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = "Rotate";
                Mode = false;
            }
        }
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        if (!Mode)
        {
            button.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = "Scale";
            Mode = true;
        }
        else
        {
            button.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = "Rotate";
            Mode = false;
        }
    }
}