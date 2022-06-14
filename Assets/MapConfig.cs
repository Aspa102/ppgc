using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapConfig : MonoBehaviour
{
    public Button button;
    public GameObject menu;
    public GameObject comp;
    public ContextMenu help;
    private bool debounce;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        if (!debounce)
        {
            help.debounce2debounceharder = true;
            comp.SetActive(false);
            menu.SetActive(true);
            debounce = true;
        }
        else
        {
            help.debounce2debounceharder = false;
            comp.SetActive(true);
            menu.SetActive(false);
            debounce = false;
        }
    }
}
