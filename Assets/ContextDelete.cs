using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextDelete : ContextObject
{
    public override void Click()
    {
        Camera.main.gameObject.GetComponent<ContextMenu>().debounce = false;
        foreach (GameObject help in ToDelete)
        {
            GameObject.Destroy(help);
        }
        Erase();
    }
}
