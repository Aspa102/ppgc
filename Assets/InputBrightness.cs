using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputBrightness : MonoBehaviour
{
    public GameObject ToChange;
    public GameObject Gui;
    public GameObject input;

    private void Update()
    {
        if (input.GetComponent<TMP_InputField>().text.Length > 0 && input.GetComponent<TMP_InputField>().isFocused && Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ///Dead code
        }
    }
}
