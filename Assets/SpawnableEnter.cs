using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnableEnter : MonoBehaviour
{
    public GameObject UnknownPrefab;
    public Transform Map;
    public SpawnableDirection dir;
    public Button Button;
    public TMP_InputField field;
    public SpawnableFroze froze;
    public GameObject Gui;

    private void Start()
    {
        Button.onClick.AddListener(Enter);
    }

    private void Enter()
    {
        var bruh = Instantiate<GameObject>(UnknownPrefab, Map);
        bruh.name = "Unknown";
        bruh.SetActive(true);
        bruh.transform.position = (Vector2)Camera.main.transform.position;
        bruh.GetComponent<SpawnablePrefab>().FacingRight = dir.FacingRight;
        bruh.GetComponent<SpawnablePrefab>().Prefab = field.text;
        bruh.GetComponent<SpawnablePrefab>().Frozen = froze.Froze;
        Gui.SetActive(false);
    }
}
