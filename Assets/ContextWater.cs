using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContextWater : MonoBehaviour
{
    public Button button;
    public List<GameObject> ToChange = new List<GameObject>();
    public GameObject Gui;
    public List<GameObject> buttons = new List<GameObject>();
    public ContextMenu menu;
    private bool debounce = false;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        foreach (GameObject help in buttons)
        {
            help.GetComponent<WaterSet>().ToChange = ToChange;
            help.SetActive(true);
        }
    }
}
