using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContextCollision : MonoBehaviour
{
    public Button button;
    public List<GameObject> ToMove = new List<GameObject>();
    public GameObject Gui;
    public GameObjectCreation GOC;
    public GameObject Text;
    public ContextMenu menu;
    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void Update()
    {
        if (ToMove.Count >= 1)
        {
            Text.GetComponent<TMP_Text>().text = "Collision: " + !ToMove[0].GetComponent<CollisionThing>().Collision;
        }
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        Camera.main.gameObject.GetComponent<ContextMenu>().debounce = false;
        foreach (GameObject Objecty in ToMove)
        {
            Objecty.transform.Find("Outline").gameObject.SetActive(false);
            if (ToMove[0].GetComponent<CollisionThing>().Collision == false)
            {
                Objecty.GetComponent<CollisionThing>().Collision = true;
            }
            else
            {
                Objecty.GetComponent<CollisionThing>().Collision = false;
            }
        }
        Gui.SetActive(false);
        ToMove.Clear();
        menu.contdel.ToDelete.Clear();
        menu.converty.ToConvert.Clear();
        menu.contdel.ToDelete.Clear();
        menu.copy.ToCopy.Clear();
        menu.brighty.ToChange.Clear();
        menu.colory.ToChange.Clear();
        menu.ordery.ToDelete.Clear();
        menu.party.ToChange.Clear();
        menu.layery.ToMove.Clear();
        menu.watery.ToChange.Clear();
        menu.Previous.Clear();
        menu.Heck.Clear();
    }
}
