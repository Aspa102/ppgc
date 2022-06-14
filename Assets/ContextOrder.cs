using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ContextOrder : ContextObject
{
    public TMP_InputField input;
    private bool debounce = false;

    public override void Click()
    {
        if (!debounce)
        {
            input.gameObject.SetActive(true);
            input.text = "0";
            debounce = true;
        }
        else
        {
            foreach (GameObject help in ToDelete)
            {
                if (!help)
                {
                    continue;
                }
                help.GetComponent<SpriteRenderer>().sortingOrder = int.Parse(input.text);
                if (help.transform.Find("Outline"))
                {
                    help.transform.Find("Outline").gameObject.SetActive(false);
                } 
            }
            input.gameObject.SetActive(false);
            Erase();
            debounce = false;
        }
    }
}
