using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using SFB;

public class Load : MonoBehaviour
{
    public Button button;
    public GameObject LoadPrefab;
    public GameObject Map;
    public BorderSettings border;
    public BackgroundSettings background;
    public AmbienceClips amb;
    public TMP_InputField one;
    public TMP_InputField two;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        LoadGame();
    }

    public void LoadGame()
    {
        var thing = Application.persistentDataPath;
        if (!Directory.Exists(thing + "/Saves"))
        {
            Directory.CreateDirectory(thing + "/Saves");
        }

        var _path = StandaloneFileBrowser.OpenFilePanel("Load Map", thing + "/Saves", "", false);
        if (_path.Length > 0)
        {
            int children = Map.transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                Destroy(Map.transform.GetChild(i).gameObject);
            }

            var path = _path[0];
            var file = File.ReadAllText(path);
            ObjectState[] Objects = JsonHelper.FromJson<ObjectState>(file);
            foreach (ObjectState Object in Objects)
            {
                CreateTheGameObjects(Object);
                Debug.Log(Object.Name);
            }
            if (Objects.Length > 0)
            {
                var obj = Objects[0];
                background.COLOR = obj.BackgroundColor;
                background.USINGIMAGE = obj.BackgroundImage;
                background.DEFAULT = obj.IsBackgroundNotUsed;
                border.COLOR = obj.BorderColor;
                border.USINGIMAGE = obj.BorderImage;
                border.DEFAULT = obj.IsBordersNotUsed;
                var texture = new Texture2D(obj.BackgroundSizeW, obj.BackgroundSizeH);
                texture.LoadImage(obj.BackgroundSprite);
                var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, obj.BackgroundSizeW, obj.BackgroundSizeH), new Vector2(0.5f, 0.5f), 35f);
                background.IMAGE = sprite;
                var texture2 = new Texture2D(obj.BorderSizeW, obj.BorderSizeH);
                texture2.LoadImage(obj.BorderSprite);
                var sprite2 = Sprite.Create(texture2, new Rect(0.0f, 0.0f, obj.BorderSizeW, obj.BorderSizeH), new Vector2(0.5f, 0.5f), 35f);
                border.IMAGE = sprite2;
                string col1 = background.COLOR.Replace("new Color32(", "");
                col1 = col1.Replace(")", "");
                col1 = col1.Replace(";&", "");
                var lastIndex = col1.LastIndexOf(", 255");
                col1 = col1.Substring(0, lastIndex);
                string col2 = border.COLOR.Replace("new Color32(", "");
                col2 = col2.Replace(")", "");
                col2 = col2.Replace(";&", "");
                var lastIndex2 = col2.LastIndexOf(", 255");
                col2 = col2.Substring(0, lastIndex2);
                one.text = col1;
                two.text = col2;
            }
        }
    }

    public void CreateTheGameObjects(ObjectState state)
    {
        var gameObject = Instantiate<GameObject>(LoadPrefab, Map.transform);
        gameObject.name = state.Name;
        gameObject.transform.position = state.Pos;
        gameObject.transform.localScale = state.Scale;
        gameObject.transform.rotation = Quaternion.Euler(state.RotEuler);
        gameObject.GetComponent<LayerThing>().Layer = state.Layer;
        gameObject.GetComponent<CollisionThing>().Collision = state.Collison;
        gameObject.GetComponent<TypeOfWater>().Type = state.WaterType;
        gameObject.GetComponent<TypeOfParticles>().type = state.ParticleType;
        gameObject.GetComponent<Brightness>().brightness = state.LightRange;
        gameObject.GetComponent<TheColorOfTheObject>().Colorhelp = state.LightColor;
        gameObject.GetComponent<SpawnablePrefab>().FacingRight = state.FacingRight;
        gameObject.GetComponent<SpawnablePrefab>().Prefab = state.SpawnableName;
        gameObject.GetComponent<SpawnablePrefab>().Frozen = state.Frozen;
        var texture = new Texture2D(state.width, state.height);
        texture.LoadImage(System.IO.File.ReadAllBytes(Application.persistentDataPath + "/Images/" + state.sprite + ".png"));
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, state.width, state.height), new Vector2(0.5f, 0.5f), state.ImgSize);
        sprite.name = state.SpriteName + "Loaded" + (int)UnityEngine.Random.Range(1f, 950000000f);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().color = state.ImgColor;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = state.Order;
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.layer = state.ObjectLayer;
        gameObject.SetActive(true);
        if (gameObject.transform.Find("Outline"))
        {
            gameObject.transform.Find("Outline").gameObject.SetActive(false);
        }
    }
}
