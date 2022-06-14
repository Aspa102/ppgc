using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnableDirection : OnClick
{
    public TMP_Text text;
    public bool FacingRight = true;
    public override void TaskOnClick()
    {
        FacingRight = !FacingRight;
        text.text = "Facing Right: " + FacingRight.ToString();
    }
}
