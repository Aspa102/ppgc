using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCreation : MonoBehaviour
{
    private GameObject bruh;
    public GameObject parentthing;
    public GameObject Square;
    public GameObject Phys;
    public GameObject Tri;
    public GameObject Circle;
    public List<GameObject> CurrentGameObject = new List<GameObject>();
    public ContextDelete del;
    public AudioSource aud;
    public AudioClip bleep;
    public GameObject Map;
    public GameObject GUI;
    public GameObject PrefabGUI;
    public float heat = 60f;
    private Vector3 origin;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GUI.activeInHierarchy == false && PrefabGUI.activeInHierarchy == false)
        {
            Create(CurrentGameObject, Input.mousePosition);
            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void Create(List<GameObject> Type, Vector2 mousePosition)
    {
        del.ToDelete.Clear();
        foreach (GameObject thing in Type)
        {
            bruh = Instantiate<GameObject>(thing, parentthing.transform);
            bruh.tag = thing.tag;
            bruh.SetActive(true);
            if (bruh.GetComponent<IgnoreUI>() == null)
            {
                bruh.AddComponent<IgnoreUI>();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bruh.transform.position = TransformSnap.GetSharedSnapPosition(new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x, Camera.main.ScreenToWorldPoint(mousePosition).y, 0f), 0.3f);
            }
            else
            {
                bruh.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(mousePosition).x, Camera.main.ScreenToWorldPoint(mousePosition).y, 0f);
            }
            if (thing.GetComponent<Positional>() != null)
            {
                bruh.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f)) + (Vector2)thing.GetComponent<Positional>().pos;
            }
            if (bruh.GetComponent<ParticleSystem>())
            {
            }
            bruh.transform.localScale = thing.transform.localScale;
            bruh.name = thing.name;
            bruh.layer = thing.layer;
            if (thing.name != "Light" && thing.name != "Particles")
            {
                if (bruh.GetComponent<LayerThing>() == null && bruh.GetComponent<CollisionThing>() == null)
                {
                    bruh.AddComponent<LayerThing>().Layer = thing.GetComponent<LayerThing>().Layer;
                    bruh.AddComponent<CollisionThing>().Collision = thing.GetComponent<CollisionThing>().Collision;
                }
            }
            if (thing.CompareTag("Custom"))
            {
                var particles = Instantiate<GameObject>((GameObject)Resources.Load("ParticlesPrefab"), bruh.transform);
                var shape = particles.GetComponent<ParticleSystem>().shape;
                shape.texture = bruh.GetComponent<SpriteRenderer>().sprite.texture;
                shape.spriteRenderer = bruh.GetComponent<SpriteRenderer>();
            }
        }
    }
}

