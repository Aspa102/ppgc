using UnityEngine;
using System.Collections;
using System;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Drag : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector2 offset;
    private bool debounce = false;
    float rotationClamp = 15f;
    Quaternion actualRotation;
    public bool Mode;
    private GameObject scale;
    public bool selected = false;
    public GameObject gui;
    public ContextDelete contdel;
    public ContextConvert converty;
    public ContextBrightness brighty;
    public ContextMenu help;
    public ContextColor colory;
    public GameObject scaleamount;
    public bool Unselect = false;
    private float heat;
    private float flipbool = 0;
    private float heat2 = 0f;

    private void Awake()
    {
        scale = GameObject.Find("ButtonScale");
    }

    private void Update()
    {
        Mode = scale.GetComponent<ChangeMode>().Mode;
        if (debounce)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !Mode)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 15f);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + -15f);
                }
            }
            else if (!Mode)
            {
                transform.Rotate(new Vector3(0, 0, Input.GetAxisRaw("Horizontal") * -0.7f));
            }

            if (Mode)
            {
                var scalesnap = float.Parse(scaleamount.GetComponent<TMP_InputField>().text);
                var blocksnap = scalesnap * 2;
                if (Input.GetKey(KeyCode.LeftShift) && gameObject.name == "Other")
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        transform.localScale = new Vector3(transform.localScale.x + 42f * blocksnap, transform.localScale.y, transform.localScale.z);
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        transform.localScale = new Vector3(transform.localScale.x - 42f * blocksnap, transform.localScale.y, transform.localScale.z);
                    }
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 42f * blocksnap, transform.localScale.z);
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 42f * blocksnap, transform.localScale.z);
                    }
                } else if (Input.GetKey(KeyCode.LeftShift) && gameObject.name != "Other" && gameObject.name != "Water")
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        transform.localScale = new Vector3(transform.localScale.x + scalesnap, transform.localScale.y, transform.localScale.z);
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        transform.localScale = new Vector3(transform.localScale.x - scalesnap, transform.localScale.y, transform.localScale.z);
                    }
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + scalesnap, transform.localScale.z);
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - scalesnap, transform.localScale.z);
                    }
                }
                else if (Input.GetKey(KeyCode.LeftShift) && gameObject.name == "Water")
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        transform.localScale = new Vector3(transform.localScale.x + 2.4f * scalesnap, transform.localScale.y, transform.localScale.z);
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        transform.localScale = new Vector3(transform.localScale.x - 2.4f * scalesnap, transform.localScale.y, transform.localScale.z);
                    }
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + 2.4f * scalesnap, transform.localScale.z);
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - 2.4f * scalesnap, transform.localScale.z);
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.localScale = new Vector3(transform.localScale.x + scalesnap, transform.localScale.y, transform.localScale.z);
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        transform.localScale = new Vector3(transform.localScale.x - scalesnap, transform.localScale.y, transform.localScale.z);
                    }
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + scalesnap, transform.localScale.z);
                    }
                    else if (Input.GetKey(KeyCode.S))
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - scalesnap, transform.localScale.z);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }

            

            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (flipbool == 0)
                {
                    if (heat2 < 5f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0f);
                        heat2 = 20f;
                        flipbool++;
                    }
                }
                if (flipbool == 1)
                {
                    if (heat2 < 5f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, 0f);
                        heat2 = 20f;
                        flipbool++;
                    }
                }
                if (flipbool == 2)
                {
                    if (heat2 < 5f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 0f);
                        heat2 = 20f;
                        flipbool++;
                    }
                }
                if (flipbool == 3)
                {
                    if (heat2 < 5f)
                    {
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, 0f);
                        heat2 = 20f;
                        flipbool = 0;
                    }
                }

            }
            if (heat2 >= 0)
            {
                heat2 -= 1;
            }
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        }

        if (Input.GetMouseButtonDown(0) && Unselect && !Input.GetKey(KeyCode.LeftControl) && !MouseOverGUIIgnores())
        {
            var select = base.GetComponent<OutlineCreater>().selectionOutlineObject;
            help.Heck.Clear();
            Unselect = false;
            select.SetActive(false);
        }
    }

    public bool MouseOverGUIIgnores()
    {
        PointerEventData help = new PointerEventData(EventSystem.current);
        help.position = Input.mousePosition;
        List<RaycastResult> ray = new List<RaycastResult>();
        EventSystem.current.RaycastAll(help, ray);
        for (int i = 0; i < ray.Count; i++)
        {
            if (ray[i].gameObject.GetComponent<IgnoreUI>() != null)
            {
                ray.RemoveAt(i);
                i--;
            }
        }
        return ray.Count > 0;
    }

    void OnMouseDown()
    {
        var select = base.GetComponent<OutlineCreater>().selectionOutlineObject;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        debounce = true;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            help.Heck.Add(gameObject);
            Unselect = true;
            select.SetActive(true);
        }
        foreach (GameObject obj in help.Heck)
        {
            if (obj.GetComponent<Positional>() != null)
            {
                obj.GetComponent<Positional>().pos = obj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            }
        }
    }

    private void OnMouseUp()
    {
        debounce = false;
    }

    private void OnMouseOver()
    {
        var select = base.GetComponent<OutlineCreater>().selectionOutlineObject;
        select.SetActive(true);
        selected = true;
        if (!gui.activeInHierarchy && !help.Heck.Contains(gameObject) && !Unselect && !Input.GetKey(KeyCode.LeftControl))
        {
            help.Heck.Add(gameObject);
        }
    }

    private void OnMouseExit()
    {
        var select = base.GetComponent<OutlineCreater>().selectionOutlineObject;
        if (!gui.activeInHierarchy && !Unselect)
        {
            help.Heck.Remove(gameObject);
            selected = false;
        }
        if (!help.Heck.Contains(gameObject))
        {
            select.SetActive(false);
        }
    }

    private bool debouncey = false;

    void OnMouseDrag()
    {
        if (Input.GetKey(KeyCode.LeftShift) && help.Heck.Count <= 1)
        {
            ///Vector3 curScreenPoint = TransformSnap.GetSharedSnapPosition(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f));
            Vector3 curScreenPoint = TransformSnap.GetSharedSnapPosition((Vector2)new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f) + offset);
            transform.position = curScreenPoint;
        }
        else if (help.Heck.Count <= 1)
        {
            Vector3 curScreenPoint = (Vector2)new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f) + offset;
            transform.position = curScreenPoint;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                foreach (GameObject obj in help.Heck)
                {
                    if (obj.GetComponent<Positional>())
                    {
                        Vector3 curScreenPoint = TransformSnap.GetSharedSnapPosition(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f) + (Vector3)obj.GetComponent<Positional>().pos);
                        obj.transform.position = curScreenPoint;
                    }
                    if (obj.GetComponent<Positional>() == null)
                    {
                        obj.AddComponent<Positional>().pos = obj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                    }
                }
            }
            else
            {
                foreach (GameObject obj in help.Heck)
                {
                    if (obj.GetComponent<Positional>() != null)
                    {
                        Vector3 curScreenPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0f) + (Vector3)obj.GetComponent<Positional>().pos;
                        obj.transform.position = curScreenPoint;
                    }
                    if (obj.GetComponent<Positional>() == null)
                    {
                        obj.AddComponent<Positional>().pos = obj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
                    }
                }
            }
        }
    }

}

public static class TransformSnap
{
    public static GameObject Thing = GameObject.Find("Snap/Input");
    // snap to this value
    ///public static float snapy = float.Parse(Thing.GetComponent<TMP_InputField>().text);


    public static Vector2 GetSharedSnapPosition(Vector3 originalPosition, float snap = 0.3f)
    {
        float snapy = float.Parse(Thing.GetComponent<TMP_InputField>().text);
        return new Vector2(GetSnapValue(originalPosition.x, snapy), GetSnapValue(originalPosition.y, snapy));
    }

    public static float GetSnapValue(float value, float snap = 0.3f)
    {
        float snapy = float.Parse(Thing.GetComponent<TMP_InputField>().text);
        return (!Mathf.Approximately(snapy, 0f)) ? Mathf.RoundToInt(value / snapy) * snapy : value;
    }
}
