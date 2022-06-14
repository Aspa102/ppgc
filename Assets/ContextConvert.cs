using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContextConvert : MonoBehaviour
{
    public Button button;
    public List<GameObject> ToConvert = new List<GameObject>();
    public GameObject Gui;
    public GameObject text;
    public ContextMenu menu;
    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (ToConvert.Count >= 1)
        {
            if (ToConvert[0].name == "PhysicsObject" && ToConvert.Count <= 1)
            {
                text.GetComponent<TMP_Text>().text = "Convert to Anchored";
            }
            else if (ToConvert.Count <= 1)
            {
                text.GetComponent<TMP_Text>().text = "Convert to Physics";
            }
            else
            {
                text.GetComponent<TMP_Text>().text = "Flip Physics / Anchored";
            }
        }
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        foreach (GameObject help in ToConvert)
        {
            if (help.name != "PhysicsObject")
            {
                help.name = "PhysicsObject";
            }
            else if (help.name == "PhysicsObject")
            {
                help.name = "Custom";
            }
            help.transform.Find("Outline").gameObject.SetActive(false);
        }
        Camera.main.gameObject.GetComponent<ContextMenu>().debounce = false;
        Gui.SetActive(false);
        menu.Heck.Clear();
        menu.collidy.ToMove.Clear();
        menu.contdel.ToDelete.Clear();
        menu.copy.ToCopy.Clear();
        menu.brighty.ToChange.Clear();
        menu.colory.ToChange.Clear();
        menu.party.ToChange.Clear();
        menu.layery.ToMove.Clear();
        menu.ordery.ToDelete.Clear();
        menu.watery.ToChange.Clear();
        menu.Previous.Clear();
        ToConvert.Clear();
    }
}
