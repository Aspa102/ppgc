using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ConfigReset : MonoBehaviour
{
    public BorderSettings border;
    public BackgroundSettings background;
    public Button button;
    public TMP_InputField one;
    public TMP_InputField two;
    public AmbienceClips amb;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        border.DEFAULT = true;
        background.DEFAULT = true;
        one.text = "";
        two.text = "";
        amb.aud.Clear();
    }

}
