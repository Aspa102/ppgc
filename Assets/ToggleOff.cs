using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOff : OnClick
{
    public GameObject Gui;
    public override void TaskOnClick()
    {
        Gui.SetActive(false);
    }
}
