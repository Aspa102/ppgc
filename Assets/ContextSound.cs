using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;

public class ContextSound : MonoBehaviour
{
    public Button button;
    public GameObject ToAdd;
    public GameObject Gui;
    private string _path;
    private AudioClip Audi;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        ToAdd.AddComponent<AudioOptIn>();
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "png", false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
        Gui.SetActive(false);
    }
    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        ///output.texture = loader.texture;
        Audi = loader.GetAudioClip();
        ToAdd.GetComponent<AudioOptIn>().Audioh = Audi;
    }
}

public class AudioOptIn : MonoBehaviour
{
    public AudioClip Audioh;
}
