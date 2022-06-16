using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using SFB;

public class CompileIntoMap : MonoBehaviour
{
    public TextAsset modjsonfile;
    public Save Savey;
    public BorderRemoval BorderRem;
    public TextAsset somestuff;
    public Texture2D TemplateTexture;
    public Texture2D BlackTileTexture;
    public Texture2D LavaTexture;
    public Texture2D AcidTexture;
    public AudioClip AcidHiss;
    public BorderSettings border;
    public BackgroundSettings background;
    public TextAsset watercode;
    public AmbienceClips amb;
    public GameObject Map;
    public Sprite CustomSprites;
    public GameObject status;
    private bool IsWater;
    private string _path;

    public void BeginCompile(string Name, string By, string Desc, Texture2D image)
    {
        /// Dialog
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        _path = StandaloneFileBrowser.SaveFilePanel("Save File", "", Name, "");
        if (_path.Length <= 0)
        {
            status.SetActive(false);
            return;
        }
        ///
        var desktoppath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var NewFolder = Directory.CreateDirectory(_path);
        var newpathimlazy = _path;
        var jsonthings = modjsonfile.text.ToString().Replace("///NAME", Name);
        var replaceyboi = jsonthings.Replace("///BYLINE", By);
        var finaljson = replaceyboi.Replace("///DESC", Desc);

        using (StreamWriter jsonfile = File.CreateText(newpathimlazy + "/mod.json"))
        {
            jsonfile.WriteLine(finaljson);
        }

        byte[] imagestuff = image.EncodeToPNG();
        File.WriteAllBytes(newpathimlazy + "/TEMPLATE.png", imagestuff);

        byte[] imagestuff2 = BlackTileTexture.EncodeToPNG();
        File.WriteAllBytes(newpathimlazy + "/black.png", imagestuff2);

        byte[] imagestuff3 = LavaTexture.EncodeToPNG();
        File.WriteAllBytes(newpathimlazy + "/Lava.png", imagestuff3);

        byte[] imagestuff4 = AcidTexture.EncodeToPNG();
        File.WriteAllBytes(newpathimlazy + "/Acid.png", imagestuff4);

        string Pathy;
        WavUtility.FromAudioClip(AcidHiss, out Pathy, true, newpathimlazy + "/Hiss", newpathimlazy + "/Hiss");

        GenerateMainScript(newpathimlazy);
        this.StartCoroutine(AddGameObjectsToMain(newpathimlazy + "/main.cs", newpathimlazy, Name));
        //Savey.StartCoroutine(Savey.SaveGame(false, newpathimlazy + "/MapBackUp.json"));
    }
    private void GenerateMainScript(string Path)
    {
        using (StreamWriter code = File.CreateText(Path + "/main.cs"))
        {
            code.WriteLine(somestuff.text.ToString());
        }
    }

    private IEnumerator AddGameObjectsToMain(string Path, string Path2, string Name)
    {
        List<GameObject> blocks = new List<GameObject>();

        int children = Map.transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            blocks.Add(Map.transform.GetChild(i).gameObject);
        }

        string awholelotofcharacters = "";
        var last = new List<string>();
        var last2 = new List<string>();
        string evenmorecharacters = "";
        foreach (GameObject block in blocks)
        {
            var bruh = "";
            if (block.name == "PhysicsObject")
            {
                if (block.GetComponent<AudioOptIn>())
                {
                    bruh = "CreatePhysicsGameObjects(" + "\u0022" + block.name + "\u0022" + ", new Vector2(" + block.transform.position.x + "f, " + block.transform.position.y + "f), new Vector2(" + block.transform.localScale.x + "f, " + block.transform.localScale.y + "f), Quaternion.Euler(0f, 0f, " + block.transform.eulerAngles.z + "f), " + block.GetComponent<SpriteRenderer>().sprite.name.Replace(" ", String.Empty) + ", \u0022" + block.GetComponent<LayerThing>().Layer + "\u0022, " + block.GetComponent<CollisionThing>().Collision.ToString().ToLower() + ");&";
                }
                else
                {
                    bruh = "CreatePhysicsGameObjects(" + "\u0022" + block.name + "\u0022" + ", new Vector2(" + block.transform.position.x + "f, " + block.transform.position.y + "f), new Vector2(" + block.transform.localScale.x + "f, " + block.transform.localScale.y + "f), Quaternion.Euler(0f, 0f, " + block.transform.eulerAngles.z + "f), " + block.GetComponent<SpriteRenderer>().sprite.name.Replace(" ", String.Empty) + ", \u0022" + block.GetComponent<LayerThing>().Layer + "\u0022, " + block.GetComponent<CollisionThing>().Collision.ToString().ToLower() + ");&";
                }
                
                byte[] imagestuff = block.GetComponent<SpriteRenderer>().sprite.texture.EncodeToPNG();
                File.WriteAllBytes(Path2 + "/" + block.GetComponent<SpriteRenderer>().sprite.name + ".png", imagestuff);
                var pleasehelp = "public static Sprite " + block.GetComponent<SpriteRenderer>().sprite.name.Replace(" ", String.Empty) + " = ModAPI.LoadSprite(\u0022" + block.GetComponent<SpriteRenderer>().sprite.name + ".png\u0022);PEEN";
                if (!last.Contains(pleasehelp) && !last2.Contains(pleasehelp))
                {
                    evenmorecharacters += pleasehelp;
                    last.Add(pleasehelp);
                }
            } else if (block.name == "Custom" || block.name == "Custom2")
            {
                bruh = "CreateCustomGameObjects(" + "\u0022" + block.name + "\u0022" + ", new Vector2(" + block.transform.position.x + "f, " + block.transform.position.y + "f), new Vector2(" + block.transform.localScale.x + "f, " + block.transform.localScale.y + "f), Quaternion.Euler(0f, 0f, " + block.transform.eulerAngles.z + "f), " + block.GetComponent<SpriteRenderer>().sprite.name.Replace(" ", String.Empty) + ", \u0022" + block.GetComponent<LayerThing>().Layer + "\u0022, " + block.GetComponent<CollisionThing>().Collision.ToString().ToLower() + ", " + block.GetComponent<SpriteRenderer>().sortingOrder + ");&";
                byte[] imagestuff = block.GetComponent<SpriteRenderer>().sprite.texture.EncodeToPNG();
                File.WriteAllBytes(Path2 + "/" + block.GetComponent<SpriteRenderer>().sprite.name + ".png", imagestuff);
                var pleasehelp = "public static Sprite " + block.GetComponent<SpriteRenderer>().sprite.name.Replace(" ", String.Empty) + " = ModAPI.LoadSprite(\u0022" + block.GetComponent<SpriteRenderer>().sprite.name + ".png\u0022);PEEN";
                if (!last2.Contains(pleasehelp) && !last.Contains(pleasehelp))
                {
                    evenmorecharacters += pleasehelp;
                    last2.Add(pleasehelp);
                }
            }
            else if (block.name == "Unknown")
            {
                bruh = "CreateSpawnable(\u0022" + block.GetComponent<SpawnablePrefab>().Prefab + "\u0022, new Vector2(" + block.transform.position.x + "f, " + block.transform.position.y + "f), new Vector2(" + block.transform.localScale.x + "f, " + block.transform.localScale.y + "f), Quaternion.Euler(0f, 0f, " + block.transform.eulerAngles.z + "f), " + block.GetComponent<SpawnablePrefab>().FacingRight.ToString().ToLower() + ", " + block.GetComponent<SpawnablePrefab>().Frozen.ToString().ToLower() + ");&";
            }
            else if (block.name == "Light")
            {
                bruh = "CreateLightSources(" + "\u0022" + block.name + "\u0022" + ", new Vector2(" + block.transform.position.x + "f, " + block.transform.position.y + "f), new Vector2(" + block.transform.localScale.x + "f, " + block.transform.localScale.y + "f), Quaternion.Euler(0f, 0f, " + block.transform.eulerAngles.z + "f), " + block.GetComponent<TheColorOfTheObject>().Colorhelp + ", " + block.GetComponent<Brightness>().brightness + "f);&";
            }
            else if (block.name == "Particles")
            {
                bruh = "CreateParticleSources(" + "\u0022" + block.name + "\u0022" + ", new Vector2(" + block.transform.position.x + "f, " + block.transform.position.y + "f), " + (block.transform.localScale.x + block.transform.localScale.y) +"f, \u0022" + block.GetComponent<TypeOfParticles>().type + "\u0022);&";
            }
            else if (block.name == "Water")
            {
                IsWater = true;
                bruh = "CreateWaterBlock(new Vector3(" + block.transform.localScale.x + "f, " + block.transform.localScale.y + "f, 1f), new Vector3(" + block.transform.position.x + "f, " + block.transform.position.y + "f, 0f), Quaternion.Euler(0f, 0f, " + block.transform.eulerAngles.z + "f), \u0022" + block.GetComponent<TypeOfWater>().Type + "\u0022);&";
            }
            else
            {
                bruh = "CreateGameObjects(" + "\u0022" + block.name + "\u0022" + ", new Vector2(" + block.transform.position.x + "f, " + block.transform.position.y + "f), new Vector2(" + block.transform.localScale.x + "f, " + block.transform.localScale.y + "f), Quaternion.Euler(0f, 0f, " + block.transform.eulerAngles.z + "f), \u0022" + block.GetComponent<LayerThing>().Layer + "\u0022, " + block.GetComponent<CollisionThing>().Collision.ToString().ToLower() + ", " + block.GetComponent<SpriteRenderer>().sortingOrder + ");&";
            }
            awholelotofcharacters += bruh;
            yield return new WaitForSeconds(0.1f);
        }
        string str = File.ReadAllText(Path);
        if (border.DEFAULT == false)
        {
            if (!border.USINGIMAGE)
            {
                awholelotofcharacters += "GameObject.Find(\u0022Root/Left wall\u0022).GetComponent<SpriteRenderer>().color = " + border.COLOR;
                awholelotofcharacters += "GameObject.Find(\u0022Root/Right wall\u0022).GetComponent<SpriteRenderer>().color = " + border.COLOR;
                awholelotofcharacters += "GameObject.Find(\u0022Root/Ceiling\u0022).GetComponent<SpriteRenderer>().color = " + border.COLOR;
                awholelotofcharacters += "GameObject.Find(\u0022Root\u0022).GetComponent<SpriteRenderer>().color = " + border.COLOR;
            }
            else
            {
                byte[] imagestuff = border.IMAGE.texture.EncodeToPNG();
                File.WriteAllBytes(Path2 + "/" + border.IMAGE.name + ".png", imagestuff);
                var pleasehelp = "public static Sprite " + border.IMAGE.name.Replace(" ", String.Empty) + " = ModAPI.LoadSprite(\u0022" + border.IMAGE.name + ".png\u0022);PEEN";
                if (!last2.Contains(pleasehelp) && !last.Contains(pleasehelp))
                {
                    evenmorecharacters += pleasehelp;
                    last2.Add(pleasehelp);
                }
                awholelotofcharacters += "GameObject.Find(\u0022Root/Left wall\u0022).GetComponent<SpriteRenderer>().sprite = Mod." + border.IMAGE.name + ";&";
                awholelotofcharacters += "GameObject.Find(\u0022Root/Right wall\u0022).GetComponent<SpriteRenderer>().sprite = Mod." + border.IMAGE.name + ";&";
                awholelotofcharacters += "GameObject.Find(\u0022Root/Ceiling\u0022).GetComponent<SpriteRenderer>().sprite = Mod." + border.IMAGE.name + ";&";
                awholelotofcharacters += "GameObject.Find(\u0022Root\u0022).GetComponent<SpriteRenderer>().sprite = Mod." + border.IMAGE.name + ";&";
            }
        }
        if (background.DEFAULT == false)
        {

            if (!background.USINGIMAGE)
            {
                awholelotofcharacters += "CreateBackground(\u0022Background\u0022, " + background.COLOR + ", null, false);&";
            }
            else
            {
                byte[] imagestuff = background.IMAGE.texture.EncodeToPNG();
                File.WriteAllBytes(Path2 + "/" + background.IMAGE.name + ".png", imagestuff);
                var pleasehelp = "public static Sprite " + background.IMAGE.name.Replace(" ", String.Empty) + " = ModAPI.LoadSprite(\u0022" + background.IMAGE.name + ".png\u0022);PEEN";
                if (!last2.Contains(pleasehelp) && !last.Contains(pleasehelp))
                {
                    evenmorecharacters += pleasehelp;
                    last2.Add(pleasehelp);
                }
                awholelotofcharacters += "CreateBackground(\u0022Background\u0022, " + background.COLOR + ", Mod." + background.IMAGE.name + ", true);&";
            }
        }
        if (amb.aud.Count > 0)
        {
            int num = 0;
            var helpmeplease = "";
            var rand = "Audio";
            for (var i = 0; i < amb.aud.Count; i++)
            {
                var Pathy = "";
                helpmeplease += "ModAPI.LoadSound(\u0022" + rand + i + ".wav\u0022), ";
                print(amb.aud[i].ToString());
                WavUtility.FromAudioClip(amb.aud[i], out Pathy, true, Path2 + "/" + "Audio" + i, Path2 + "/" + "Audio" + i);
            }
            helpmeplease = helpmeplease.Remove(helpmeplease.Length - 2);
            if (amb.chance.text.Length > 0)
            {
                awholelotofcharacters += "AddAudioToAmbience(" + amb.chance.text + "f, new AudioClip[] {" + helpmeplease + "});&";
            }
            else
            {
                awholelotofcharacters += "AddAudioToAmbience(0.01f, new AudioClip[] {" + helpmeplease + "});&";
            }
        }
        string borders = "";
        if (BorderRem.Checked)
        {
            borders = "RemoveBorders();";
        }
        evenmorecharacters = evenmorecharacters.Replace("PEEN", System.Environment.NewLine + "        ");
        awholelotofcharacters = awholelotofcharacters.Replace("&", System.Environment.NewLine + "                ");
        print(awholelotofcharacters);
        print(evenmorecharacters);
        borders = str.Replace("///Destroy?", borders);
        string someonehelpme = borders.Replace("///CUSTOMSPRITES", evenmorecharacters);
        string aaaaahelpme = someonehelpme.Replace("///NAME", "public static string MapName = \u0022" + Name + "\u0022;");
        string imtired = aaaaahelpme.Replace("///HrrrrrgghhhhFunnyTextThatNoOneWouldGuessLolXDHAHAfunniepoopoo", awholelotofcharacters);
        File.WriteAllText(Path, imtired);
        status.SetActive(false);
    }
    
    public void WriteResult(string[] paths)
    {
        if (paths.Length == 0)
        {
            return;
        }

        _path = "";
        foreach (var p in paths)
        {
            _path += p + "\n";
        }
    }

    public void WriteResult(string path)
    {
        _path = path;
    }
}

