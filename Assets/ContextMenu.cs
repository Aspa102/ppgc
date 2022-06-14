using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    private float heat = 0f;
    public GameObject gui;
    public ContextDelete contdel;
    public ContextConvert converty;
    public ContextBrightness brighty;
    public ContextColor colory;
    public ContextParticle party;
    public ContextCollision collidy;
    public ContextLayer layery;
    public ContextWater watery;
    public ContextOrder ordery;
    public List<GameObject> Heck = new List<GameObject>();
    public List<GameObject> Previous = new List<GameObject>();
    public ContextCopy copy;
    public bool Multiple = false;
    public bool debounce = false;
    public bool debounce2debounceharder = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0) && !debounce && !debounce2debounceharder)
        {
            foreach(GameObject Objectthing in Heck)
            {
                if (Objectthing && Objectthing.GetComponent<Drag>().selected == true)
                {
                    if (Objectthing.tag == "Delete" || Objectthing.tag == "Custom")
                    {
                        if (Previous.Contains(Objectthing))
                        {
                            continue;
                        }
                        Previous.Add(Objectthing);
                        debounce = true;
                        contdel.ToDelete.Add(Objectthing);
                        converty.ToConvert.Add(Objectthing);
                        brighty.ToChange.Add(Objectthing);
                        colory.ToChange.Add(Objectthing);
                        party.ToChange.Add(Objectthing);
                        collidy.ToMove.Add(Objectthing);
                        layery.ToMove.Add(Objectthing);
                        copy.ToCopy.Add(Objectthing);
                        watery.ToChange.Add(Objectthing);
                        ordery.ToDelete.Add(Objectthing);
                        gui.SetActive(true);
                        brighty.gameObject.SetActive(true);
                        colory.gameObject.SetActive(true);
                        party.gameObject.SetActive(true);
                        converty.gameObject.SetActive(true);
                        collidy.gameObject.SetActive(true);
                        layery.gameObject.SetActive(true);
                        watery.gameObject.SetActive(false);
                        ordery.gameObject.SetActive(false);
                        if (Objectthing.name != "Light" && Objectthing.name != "Light(Clone)" && Objectthing.name != "Particles" && Objectthing.name != "Water" && Objectthing.name != "Lava")
                        {
                            brighty.gameObject.SetActive(false);
                            colory.gameObject.SetActive(false);
                            party.gameObject.SetActive(false);
                            collidy.gameObject.SetActive(true);
                            layery.gameObject.SetActive(true);
                            watery.gameObject.SetActive(false);
                            ordery.gameObject.SetActive(true);
                        }
                        else if (Objectthing.name == "Particles")
                        {
                            party.gameObject.SetActive(true);
                            converty.gameObject.SetActive(false);
                            brighty.gameObject.SetActive(false);
                            colory.gameObject.SetActive(false);
                            collidy.gameObject.SetActive(false);
                            layery.gameObject.SetActive(false);
                            watery.gameObject.SetActive(false);
                        }
                        else if (Objectthing.name == "Water")
                        {
                            party.gameObject.SetActive(false);
                            converty.gameObject.SetActive(false);
                            brighty.gameObject.SetActive(false);
                            colory.gameObject.SetActive(false);
                            collidy.gameObject.SetActive(false);
                            layery.gameObject.SetActive(false);
                            watery.gameObject.SetActive(true);
                        }
                        else
                        {
                            party.gameObject.SetActive(false);
                            converty.gameObject.SetActive(false);
                            collidy.gameObject.SetActive(false);
                            layery.gameObject.SetActive(false);
                            watery.gameObject.SetActive(false);
                        }
                        gui.GetComponent<RectTransform>().transform.position = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                        gui.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(gui.GetComponent<RectTransform>().anchoredPosition3D.x, gui.GetComponent<RectTransform>().anchoredPosition3D.y, 0f);
                        heat = 30f;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && gui.activeInHierarchy && heat < 10f && !Multiple || Input.GetMouseButton(2) && gui.activeInHierarchy && heat < 1f || Input.mouseScrollDelta.y != 0 && gui.activeInHierarchy && heat < 1f && !Multiple)
        {
            foreach (GameObject help in Heck)
            {
                help.GetComponent<Drag>().Unselect = false;
                help.GetComponent<Drag>().selected = false;
                help.transform.Find("Outline").gameObject.SetActive(false);
            }
            Previous.Clear();
            gui.SetActive(false);
            Heck.Clear();
            contdel.ToDelete.Clear();
            converty.ToConvert.Clear();
            ordery.ToDelete.Clear();
            collidy.ToMove.Clear();
            layery.ToMove.Clear();
            copy.ToCopy.Clear();
            brighty.ToChange.Clear();
            colory.ToChange.Clear();
            party.ToChange.Clear();
            watery.ToChange.Clear();
            debounce = false;
        } 
        else
        {
            debounce = false;
        }
        if (heat > 0)
        {
            heat -= 1f;
        }
    }
}
