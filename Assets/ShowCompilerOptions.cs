using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCompilerOptions : MonoBehaviour
{
    public Button button;
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
        GUIMover.SetActive(true);
        button.gameObject.SetActive(false);
    }
}
