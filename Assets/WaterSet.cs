using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaterSet : MonoBehaviour
{
    public Button button;
    public List<GameObject> ToChange = new List<GameObject>();
    public ContextWater parent;
    public ContextMenu menu;
    public GameObject gui;
    public List<GameObject> buttons = new List<GameObject>();
    public string SetTo = "Water";
    public Color32 color = new Color32(69, 82, 195, 140);

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        foreach (GameObject help in ToChange)
        {
            if (help.GetComponent<TypeOfWater>() != null)
            {
                help.GetComponent<TypeOfWater>().Type = SetTo;
                help.GetComponent<SpriteRenderer>().color = color;
            }
        }
        foreach (GameObject hh in buttons)
        {
            hh.SetActive(false);
        }
        gui.gameObject.SetActive(false);
        ToChange.Clear();
        parent.ToChange.Clear();
        menu.Heck.Clear();
        menu.contdel.ToDelete.Clear();
        menu.converty.ToConvert.Clear();
        menu.collidy.ToMove.Clear();
        menu.contdel.ToDelete.Clear();
        menu.copy.ToCopy.Clear();
        menu.brighty.ToChange.Clear();
        menu.colory.ToChange.Clear();
        menu.party.ToChange.Clear();
        menu.layery.ToMove.Clear();
        menu.watery.ToChange.Clear();
        menu.Previous.Clear();
    }
}
