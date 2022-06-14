using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HumScale : MonoBehaviour
{
    public Button button;
    public GameObject Set;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        Set.SetActive(!Set.activeInHierarchy);
    }
}
