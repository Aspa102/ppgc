using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrefabMenu : MonoBehaviour
{
    public GameObject Gui;
    public TMP_Text Text;
    public TMP_Text Text2;
    public SpawnableDirection dir;
    public GameObject Other1;
    public GameObject Other2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !Gui.activeInHierarchy && !Other1.activeInHierarchy && !Other2.activeInHierarchy)
        {
            Gui.SetActive(true);
            Text.text = "";
            Text2.text = "Facing Right: True";
            dir.FacingRight = true;
        }
    }
}
