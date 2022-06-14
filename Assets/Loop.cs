using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public Material Mat;
    public Camera Cam;

    private void FixedUpdate()
    {
        Mat.SetTextureOffset("_MainTex", (Cam.transform.position / 8) / Cam.orthographicSize);
    }
}
