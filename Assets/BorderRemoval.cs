using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderRemoval : OnClick
{
    public Sprite NoCheck;
    public Sprite Check;
    public GameObject Map;
    public bool Checked;
    public override void TaskOnClick()
    {
        Checked = !Checked;
        if (Checked == true)
        {
            this.gameObject.GetComponent<Image>().sprite = Check;
            Map.SetActive(false);
        }
        else
        {
            this.gameObject.GetComponent<Image>().sprite = NoCheck;
            Map.SetActive(true);
        }
    }
}
