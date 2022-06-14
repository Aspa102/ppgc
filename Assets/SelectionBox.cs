using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionBox : MonoBehaviour
{
    public RectTransform selectionbox;
    public ContextMenu menu;
    private bool bounce = false;
    private Vector2 pos;
    public GameObject Map;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && menu.Heck.Count <= 0 && !MouseOverGUIIgnores())
        {
            pos = Input.mousePosition;
            bounce = true;
        }
        if (Input.GetMouseButton(0) && bounce == true)
        {
            UpdateBox(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0) && bounce == true)
        {
            ReleaseBox();
            bounce = false;
        }
    }
    ///Literally a youtube tutorial because I couldn't be bothered
    private void UpdateBox(Vector2 mousepos)
    {
        if (!selectionbox.gameObject.activeInHierarchy)
        {
            selectionbox.gameObject.SetActive(true);
        }
        else
        {
            List<GameObject> blocks = new List<GameObject>();

            int children = Map.transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                blocks.Add(Map.transform.GetChild(i).gameObject);
            }

            foreach (GameObject selectable in blocks)
            {
                selectable.GetComponent<Drag>().Unselect = false;
                selectable.transform.Find("Outline").gameObject.SetActive(false);
                if (menu.Heck.Contains(selectable))
                {
                    menu.Heck.Remove(selectable);
                }
            }
        }
        float width = mousepos.x - pos.x;
        float height = mousepos.y - pos.y;
        selectionbox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionbox.anchoredPosition = pos + new Vector2(width / 2, height / 2);
    }
    private void ReleaseBox()
    {
        selectionbox.gameObject.SetActive(false);
        Vector2 min = selectionbox.anchoredPosition - (selectionbox.sizeDelta / 2);
        Vector2 max = selectionbox.anchoredPosition + (selectionbox.sizeDelta / 2);

        List<GameObject> blocks = new List<GameObject>();

        int children = Map.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            blocks.Add(Map.transform.GetChild(i).gameObject);
        }
        
        foreach (GameObject selectable in blocks)
        {
            Vector2 area = Camera.main.WorldToScreenPoint(selectable.transform.position);
            if (area.x > min.x && area.x < max.x && area.y > min.y && area.y < max.y)
            {
                selectable.GetComponent<Drag>().Unselect = true;
                selectable.GetComponent<Drag>().selected = true;
                selectable.transform.Find("Outline").gameObject.SetActive(true);
                menu.Heck.Add(selectable);
            }
        }

    }

    public bool MouseOverGUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
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

}
