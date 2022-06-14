using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableButton : OnClick
{
    public override void TaskOnClick()
    {
        Application.OpenURL("https://github.com/mestiez/ppg-snippets/blob/master/all%20spawnable%20object%20names.txt");
    }
}
