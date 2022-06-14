using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartCompile : MonoBehaviour
{
    public Button button;
    public CompileIntoMap compile;
    public GameObject comp;
    public AudioSource Aud;
    public InputField NameOf;
    public InputField ByOf;
    public InputField DescOf;
    public GameObject ThumbOf;
    public GameObject compButton;
    public GameObject ImageOf;
    public GameObject status;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        string name = NameOf.text;
        string by = ByOf.text;
        string desc = DescOf.text;
        Texture2D img = (Texture2D)ImageOf.GetComponent<RawImage>().texture;
        if (name.Length < 1)
        {
            name = "Template";
        }
        if (by.Length < 1)
        {
            by = "By Someone";
        }
        if (desc.Length < 1)
        {
            desc = "PPGC Custom Map";
        }
        comp.SetActive(false);
        compButton.SetActive(true);
        status.SetActive(true);
        compile.BeginCompile(name, by, desc, img);
    }
}
