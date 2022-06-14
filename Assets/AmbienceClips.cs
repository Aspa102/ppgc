using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using TMPro;

public class AmbienceClips : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    private string _path;
    public List<AudioClip> aud = new List<AudioClip>();
    private string rand;
    public TMP_InputField field;
    public GameObject Gui;
    public GameObject ActualGui;
    public TMP_InputField chance;
    public GameObject funny;
    public WWW loader;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "wav", true);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
    private IEnumerator OutputRoutine(string url)
    {
        loader = new WWW(url);
        yield return new WaitUntil(() => loader != null && loader.isDone);
        loader.GetAudioClip().name = "AudioFile";
        field.text = "";
        loader.GetAudioClip().LoadAudioData();
        aud.Add(loader.GetAudioClip());
        funny.SetActive(true);
        Gui.SetActive(false);
    }
}
