using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ContextObject : MonoBehaviour
{
    public Button Button;
    public List<GameObject> ToDelete = new List<GameObject>();
    public GameObject Gui;
    public ContextMenu Menu;
    public void Awake()
    {
        Button.onClick.AddListener(Click);
    }

    public abstract void Click();

    public void Erase()
    {
        ToDelete.Clear();
        Menu.Heck.Clear();
        Gui.SetActive(false);
        foreach (var context in Object.FindObjectsOfType<ContextObject>())
        {
            context.ToDelete.Clear();
        }
        Menu.contdel.ToDelete.Clear();
        Menu.converty.ToConvert.Clear();
        Menu.collidy.ToMove.Clear();
        Menu.layery.ToMove.Clear();
        Menu.brighty.ToChange.Clear();
        Menu.colory.ToChange.Clear();
        Menu.party.ToChange.Clear();
        Menu.watery.ToChange.Clear();
        Menu.copy.ToCopy.Clear();
        Menu.Previous.Clear();
    }
}
