using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 Origin; 
    private Vector3 Difference; 
    
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Origin = MousePos();
        }
        if (Input.GetMouseButton(2))
        {
            Difference = MousePos() - transform.position;
            transform.position = Origin - Difference;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1f * Time.deltaTime * 5f * Camera.main.orthographicSize, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1f * Time.deltaTime * 5f * Camera.main.orthographicSize, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0f, 1f * Time.deltaTime * 5f * Camera.main.orthographicSize, 0f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0f, -1f * Time.deltaTime * 5f * Camera.main.orthographicSize, 0f);
            }
        }
        if (Input.mouseScrollDelta.y < 0 || Input.mouseScrollDelta.y > 0)
        {
            float num = 0f;
            if (Input.mouseScrollDelta.y > 0)
            {
                num = -0.5f * Time.deltaTime * 50f * Camera.main.orthographicSize;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                num = 0.5f * Time.deltaTime * 50f * Camera.main.orthographicSize;
            }
            if (Camera.main.orthographicSize > 0.05f && Camera.main.orthographicSize < 252f)
            {
                ///var thingy = Mathf.Clamp(-Input.mouseScrollDelta.y, -1f, 1f);
                Camera.main.orthographicSize = Mathf.Clamp((Camera.main.orthographicSize + num), 0.1f, 250f);
            }
        }
    }
    // return the position of the mouse in world coordinates (helper method)
    Vector3 MousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
