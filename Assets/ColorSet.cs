using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ColorSet : MonoBehaviour
{
    public BorderSettings border;
    public BackgroundSettings background;
    public TMP_InputField input;

    private void Awake()
    {
        input.onEndEdit.AddListener(Task);
    }

    private void Task(string helpmegod)
    {
        if (input.gameObject.name == "ColorInput")
        {
            background.COLOR = "new Color32(" + helpmegod + ", 255)";
            background.USINGIMAGE = false;
            background.DEFAULT = false;
        }
        if (input.gameObject.name == "ColorInput2")
        {
            border.COLOR = "new Color32(" + helpmegod + ", 255);&";
            border.USINGIMAGE = false;
            border.DEFAULT = false;
        }
    }

}
