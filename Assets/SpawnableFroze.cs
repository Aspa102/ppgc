using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnableFroze : OnClick
{
    public bool Froze = false;
    public TMP_Text Text;

    public override void TaskOnClick()
    {
        Froze = !Froze;
        Text.text = "Frozen: " + Froze.ToString().ToLower();
    }
}
