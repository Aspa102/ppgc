using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Button button;
    public AudioSource Music;
    public bool toggle = false;
    public float value;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        StartCoroutine(AudioStuff());
    }

    private void Update()
    {
        value += 0.05f * Time.deltaTime;
    }

    private IEnumerator AudioStuff()
    {
        if (!toggle)
        {
            Music.Stop();
            toggle = true;
        }
        else
        {
            Music.Play();
            toggle = false;
        }
        yield return new WaitForSeconds(0f);
    }

}
