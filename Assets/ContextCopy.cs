using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextCopy : MonoBehaviour
{
    public Button button;
    public List<GameObject> ToCopy = new List<GameObject>();
    public GameObject Gui;
    public GameObjectCreation GOC;
    public ContextMenu menu;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        GOC.CurrentGameObject.Clear();
        Camera.main.gameObject.GetComponent<ContextMenu>().debounce = false;
        foreach (GameObject Copy in ToCopy)
        {
            Copy.GetComponent<OutlineCreater>().enabled = false;
            GameObject bruh = (GameObject)Instantiate(Copy);
            if (bruh.transform.Find("Outline"))
            {
                bruh.transform.Find("Outline").gameObject.SetActive(false);
            }
            bruh.name = Copy.name;
            bruh.transform.position = Copy.transform.position;
            bruh.SetActive(false);
            bruh.transform.SetParent(null);
            GOC.CurrentGameObject.Add(bruh);
            bruh.AddComponent<Positional>().pos = bruh.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            Copy.transform.Find("Outline").gameObject.SetActive(false);
        }
        ToCopy.Clear();
        menu.contdel.ToDelete.Clear();
        menu.converty.ToConvert.Clear();
        menu.collidy.ToMove.Clear();
        menu.layery.ToMove.Clear();
        menu.brighty.ToChange.Clear();
        menu.colory.ToChange.Clear();
        menu.party.ToChange.Clear();
        menu.watery.ToChange.Clear();
        menu.ordery.ToDelete.Clear();
        menu.Previous.Clear();
        menu.Heck.Clear();
        Gui.SetActive(false);
    }
}

public class Positional : MonoBehaviour
{
    public Vector2 pos;
}
