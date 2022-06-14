using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeItem1 : MonoBehaviour
{
    public Button button;
    public GameObject Set;
    public GameObjectCreation Cam;
    public AudioSource Aud;
    public static AudioClip blip;

    private void Awake()
    {
        blip = Resources.Load<AudioClip>("blip");
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        Aud.PlayOneShot(blip, 4f);
        Cam.CurrentGameObject.Clear();
        Cam.CurrentGameObject.Add(Set);
    }
}
