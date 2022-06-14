using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContextLayer : MonoBehaviour
{
    public Button button;
    public List<GameObject> ToMove = new List<GameObject>();
    public GameObject Gui;
    public GameObject Text;
    public GameObjectCreation GOC;
    public ContextMenu menu;
    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (ToMove.Count >= 1)
        {
            Text.GetComponent<TMP_Text>().text = "Layer: " + ToMove[0].GetComponent<LayerThing>().Layer;
        }
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        Camera.main.gameObject.GetComponent<ContextMenu>().debounce = false;
        foreach (GameObject Objecty in ToMove)
        {
            Objecty.transform.Find("Outline").gameObject.SetActive(false);
            if (ToMove[0].GetComponent<LayerThing>().Layer == "Front")
            {
                Objecty.GetComponent<LayerThing>().Layer = "Back";
            }
            else
            {
                Objecty.GetComponent<LayerThing>().Layer = "Front";
            }
        }
        Gui.SetActive(false);
        ToMove.Clear();
        menu.Heck.Clear();
        menu.contdel.ToDelete.Clear();
        menu.converty.ToConvert.Clear();
        menu.collidy.ToMove.Clear();
        menu.contdel.ToDelete.Clear();
        menu.copy.ToCopy.Clear();
        menu.brighty.ToChange.Clear();
        menu.colory.ToChange.Clear();
        menu.party.ToChange.Clear();
        menu.watery.ToChange.Clear();
        menu.ordery.ToDelete.Clear();
        menu.Heck.Clear();
        menu.Previous.Clear();
    }
}
