using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;

public class ChangeItem2 : MonoBehaviour
{
    public Button button;
    public GameObjectCreation Cam;
    private Texture2D Texturey;
    private Sprite NewSprite;
    private float num = 1f;
    public Material Mater;
    public ContextDelete contextDelete;
    public ContextConvert contextConvert;
    public GameObject Amount;
    public ContextColor contextColor;
    public ContextBrightness contextBrightness;
    public ContextMenu helpme;
    public GameObject Menu;
    public AudioSource Aud;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        Aud.PlayOneShot(ChangeItem1.blip, 4f);
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
        Texturey = loader.texture;
        Texturey.filterMode = FilterMode.Point;
        Texturey.name = System.Guid.NewGuid().ToString("N");
        //if (!System.IO.File.Exists(Application.persistentDataPath + "/Images"))
        //{
        //    System.IO.File.Create(Application.persistentDataPath + "/Images");
        //}
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/Images/" + Texturey.name + ".png", Texturey.EncodeToPNG());
        NewSprite = Sprite.Create(Texturey, new Rect(0f, 0f, (float)Texturey.width, (float)Texturey.height), 0.5f * Vector2.one, 35f);
        NewSprite.name = "CustomSprite" + num;
        num++;
        GameObject NewPrefab = new GameObject();
        NewPrefab.name = "Custom";
        NewPrefab.tag = "Custom";
        NewPrefab.layer = 7;
        NewPrefab.AddComponent<SpriteRenderer>().sprite = NewSprite;
        NewPrefab.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("Back");
        NewPrefab.AddComponent<PolygonCollider2D>();
        NewPrefab.AddComponent<Drag>();
        NewPrefab.AddComponent<OutlineCreater>().mater = Mater;
        NewPrefab.GetComponent<Drag>().help = helpme;
        NewPrefab.GetComponent<Drag>().gui = Menu;
        NewPrefab.GetComponent<Drag>().contdel = contextDelete;
        NewPrefab.GetComponent<Drag>().converty = contextConvert;
        NewPrefab.GetComponent<Drag>().colory = contextColor;
        NewPrefab.GetComponent<Drag>().brighty = contextBrightness;
        NewPrefab.GetComponent<Drag>().scaleamount = Amount;
        NewPrefab.AddComponent<LayerThing>().Layer = "Front";
        NewPrefab.AddComponent<CollisionThing>();
        NewPrefab.SetActive(false);
        Cam.CurrentGameObject.Clear();
        Cam.CurrentGameObject.Add(NewPrefab);
    }
}
