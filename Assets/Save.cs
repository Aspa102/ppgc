using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using SFB;

public class Save : MonoBehaviour
{
    public Button button;
    public bool Finished = false;
    public BorderSettings border;
    public BackgroundSettings background;
    public AmbienceClips amb;
    public GameObject Map;
    public GameObject SavingStatus;
    public TMP_InputField AutoCount;
    public float AutoInterval;
    public Tuple<string, Quaternion, Vector2, Sprite> NormalObject;
    public Tuple<string, Quaternion, Vector2, string> Water;
    public Tuple<Color32, Sprite, bool, Color32, Sprite, bool> Colors;
    public Tuple<string, Vector2, float, Color32> Lights;
    public Tuple<string, Vector2, string> Particles;
    public List<byte[]> Ambience = new List<byte[]>();
    public List<ObjectState> objects = new List<ObjectState>();
    public List<GameObject> MapObjects = new List<GameObject>();
    public float num = 1f;
    private string _path;
    private string alltext;

    private void Awake()
    {
        button.onClick.AddListener(TaskOnClick);
        AutoCount.onEndEdit.AddListener(OnTaskEnter);
        StartCoroutine(AutoSave());
        var thing = Application.persistentDataPath;
        if (!Directory.Exists(thing + "/Saves"))
        {
            Directory.CreateDirectory(thing + "/Saves");
        }
        if (!Directory.Exists(thing + "/Saves/Autosaves"))
        {
            Directory.CreateDirectory(thing + "/Saves/Autosaves");
        }
        while (File.Exists(thing + "/Saves/Autosaves/MapSave" + num + ".json"))
        {
            num++;
        }
    }

    private void TaskOnClick()
    {
        StartCoroutine(SaveGame());
    }

    private void OnTaskEnter(string num)
    {
        AutoInterval = float.Parse(num);
        StopCoroutine(AutoSave());
        StartCoroutine(AutoSave());
    }

    public IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(AutoInterval);
            if (Map.transform.childCount > 0)
            {
                StartCoroutine(SaveGame(true));
            }
        }
    }

    public IEnumerator SaveGame(bool Ignore = false, string AltPath = "")
    {
        this.Finished = false;
        print("Saved!");
        var thing = Application.persistentDataPath;
        _path = thing + "/Saves/Autosaves/MapSave" + num + ".json";
        if (!Ignore)
        {
            if (AltPath != "")
            {
                _path = AltPath;
            }
            else
            {
                _path = StandaloneFileBrowser.SaveFilePanel("Save Map", thing + "/Saves", "MapSave" + num, "json");
                if (_path.Length <= 0)
                {
                    yield return null;
                }
            }
        }
        if (Ignore)
        {
            if (File.Exists(thing + "/Saves/Autosaves/MapSave" + num + ".json"))
            {
                File.Delete(thing + "/Saves/Autosaves/MapSave" + num + ".json");
            }
        }
        else
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }
        SavingStatus.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Saving...";
        SavingStatus.SetActive(true);
        int children = Map.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            MapObjects.Add(Map.transform.GetChild(i).gameObject);
        }
        var somenumber = 0f;
        foreach (var MapObject in MapObjects)
        {
            somenumber++;
            ObjectState obj = new ObjectState
            {
                Name = MapObject.name,
                Pos = MapObject.transform.position,
                Scale = MapObject.transform.localScale,
                RotEuler = MapObject.transform.eulerAngles,
                sprite = MapObject.GetComponent<SpriteRenderer>().sprite.texture.name,
                SpriteName = MapObject.GetComponent<SpriteRenderer>().sprite.name,
                width = MapObject.GetComponent<SpriteRenderer>().sprite.texture.width,
                height = MapObject.GetComponent<SpriteRenderer>().sprite.texture.height,
                ImgSize = MapObject.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit,
                ImgColor = MapObject.GetComponent<SpriteRenderer>().color,
                ObjectLayer = MapObject.layer,
                Order = MapObject.GetComponent<SpriteRenderer>().sortingOrder
            };

            if (MapObject.name == "Light")
            {
                obj.LightColor = MapObject.GetComponent<TheColorOfTheObject>().Colorhelp;
                obj.LightRange = MapObject.GetComponent<Brightness>().brightness;
            }
            if (MapObject.name == "Particles")
            {
                obj.ParticleType = MapObject.GetComponent<TypeOfParticles>().type;
            }
            if (MapObject.name == "Water")
            {
                obj.WaterType = MapObject.GetComponent<TypeOfWater>().Type;
            }
            if (MapObject.GetComponent<LayerThing>())
            {
                obj.Layer = MapObject.GetComponent<LayerThing>().Layer;
            }
            if (MapObject.GetComponent<CollisionThing>())
            {
                obj.Collison = MapObject.GetComponent<CollisionThing>().Collision;
            }
            if (MapObject.GetComponent<SpawnablePrefab>())
            {
                obj.SpawnableName = MapObject.GetComponent<SpawnablePrefab>().Prefab;
                obj.FacingRight = MapObject.GetComponent<SpawnablePrefab>().FacingRight;
                obj.Frozen = MapObject.GetComponent<SpawnablePrefab>().Frozen;
            }
            foreach (var sound in amb.aud)
            {
                Ambience.Add(WavUtility.FromAudioClip(sound));
            }
            if (somenumber <= 1)
            {
                obj.BackgroundColor = background.COLOR;
                obj.BackgroundImage = background.USINGIMAGE;
                obj.IsBackgroundNotUsed = background.DEFAULT;
                obj.BorderColor = border.COLOR;
                obj.BorderImage = border.USINGIMAGE;
                obj.IsBordersNotUsed = border.DEFAULT;
                obj.BorderSprite = border.IMAGE.texture.EncodeToPNG();
                obj.BackgroundSprite = background.IMAGE.texture.EncodeToPNG();
                obj.BorderSizeW = border.IMAGE.texture.width;
                obj.BorderSizeH = border.IMAGE.texture.height;
                obj.BackgroundSizeW = background.IMAGE.texture.width;
                obj.BackgroundSizeH = background.IMAGE.texture.height;
            }
            this.objects.Add(obj);
            yield return new WaitForSeconds(0.1f);
        }
        string alltext = JsonHelper.ToJson(objects.ToArray(), true);
        if (alltext.Length <= 0 && alltext.Length < children)
        {
            SavingStatus.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Error. Try Again.";
            SavingStatus.AddComponent<wait>();
            MapObjects.Clear();
            objects.Clear();
            this.Finished = true;
            File.Delete(_path);
            yield break;
        }
        File.WriteAllText(_path, alltext);
        if (!Ignore)
        {
            SavingStatus.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Success!";
        }
        else
        {
            SavingStatus.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Autosaved!";
        }
        SavingStatus.AddComponent<wait>();
        MapObjects.Clear();
        objects.Clear();
        this.Finished = true;
    }
}

[Serializable]
public class ObjectState
{
    [SerializeField] public string Name;
    [SerializeField] public Vector3 Pos;
    [SerializeField] public Vector3 Scale;
    [SerializeField] public Vector3 RotEuler;
    [SerializeField] public string sprite = "bruh";
    [SerializeField] public string WaterType = "Water";
    [SerializeField] public string ParticleType = "Sparks";
    [SerializeField] public string LightColor = "Color.white";
    [SerializeField] public float LightRange = 1f;
    [SerializeField] public string Layer = "Front";
    [SerializeField] public bool Collison = true;
    [SerializeField] public int Order = 0;
    [SerializeField] public int width = 1;
    [SerializeField] public int height = 1;
    [SerializeField] public float ImgSize = 35f;
    [SerializeField] public Color ImgColor = Color.white;
    [SerializeField] public string BorderColor = "new Color32(0, 0, 0, 255)";
    [SerializeField] public string BackgroundColor = "new Color32(0, 0, 0, 255)";
    [SerializeField] public bool IsBackgroundNotUsed = true;
    [SerializeField] public bool IsBordersNotUsed = true;
    [SerializeField] public byte[] BorderSprite = new byte[0];
    [SerializeField] public byte[] BackgroundSprite = new byte[0];
    [SerializeField] public bool BackgroundImage = false;
    [SerializeField] public bool Frozen = false;
    [SerializeField] public bool BorderImage = false;
    [SerializeField] public int BorderSizeW = 16;
    [SerializeField] public int BackgroundSizeW = 16;
    [SerializeField] public int BorderSizeH = 16;
    [SerializeField] public int BackgroundSizeH = 16;
    [SerializeField] public string SpawnableName = "Human";
    [SerializeField] public bool FacingRight = true;
    [SerializeField] public string SpriteName = "null";
    [SerializeField] public int ObjectLayer;
}

[Serializable]
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
