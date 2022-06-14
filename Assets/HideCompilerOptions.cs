using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideCompilerOptions : MonoBehaviour
{
    public Button button;
    public Button other;
    public CompileIntoMap compile;
    public GameObject GUIMover;
    public AudioSource Aud;
    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }
    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        GUIMover.SetActive(false);
        other.gameObject.SetActive(true);
    }
}
