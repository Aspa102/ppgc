using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;

public class NewImageButton : MonoBehaviour
{
    public Button button;
    public AudioSource Aud;
    public GameObject Texturey;
    public RawImage output;
    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }
    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "png", false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        ///output.texture = loader.texture;
        Texturey.GetComponent<RawImage>().texture = loader.texture;
    }
}
