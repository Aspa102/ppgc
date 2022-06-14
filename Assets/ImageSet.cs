using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;

public class ImageSet : MonoBehaviour
{
    public Button button;
    public BorderSettings border;
    public BackgroundSettings background;
    public Sprite NewSprite;
    private string rand;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        ///Aud.PlayOneShot();
        var paths = StandaloneFileBrowser.OpenFilePanel("Title", "", "png", false);
        if (paths.Length > 0)
        {
            StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
        }
    }
    private IEnumerator OutputRoutine(string url)
    {
        var loader = new WWW(url);
        yield return loader;
        ///output.texture = loader.texture;
        var Texturey = loader.texture;
        Texturey.filterMode = FilterMode.Point;
        NewSprite = Sprite.Create(Texturey, new Rect(0f, 0f, (float)Texturey.width, (float)Texturey.height), 0.5f * Vector2.one, 35f);
        const string glyphs = "abcdefgaagagaghijklmngergergrtoaghgapqrstuvwhydsgshjktdsgdcxbgnragdzbgjyeirgsdfgjfgxvseyrtjdyfhcbdzgsxruzdtzhbgjrxzr6ydghzsthrjykxltufyir6itototooyoiooyorusyrfawdcvbshdjdhxghutsyusruyrxyz0123456gagagaegr789erytfugihigfdhzsrwestdyufhvkgiyfltdukrfcgjvhk";
        int charAmount = Random.Range(10, 25);
        for (int i = 0; i < charAmount; i++)
        {
            rand += glyphs[Random.Range(0, glyphs.Length)];
        }
        NewSprite.name = "Image" + rand;
        if (button.gameObject.name == "ImageBackground")
        {
            background.DEFAULT = false;
            background.IMAGE = NewSprite;
            background.USINGIMAGE = true;
        }
        if (button.gameObject.name == "ImageBorders")
        {
            border.DEFAULT = false;
            border.IMAGE = NewSprite;
            border.USINGIMAGE = true;
        }
    }
}
