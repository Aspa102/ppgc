using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeItem3 : MonoBehaviour
{
    public Button button;
    public GameObject Set;
    public GameObjectCreation Cam;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        Cam.CurrentGameObject.Clear();
        Cam.CurrentGameObject.Add(Set);
    }
}
