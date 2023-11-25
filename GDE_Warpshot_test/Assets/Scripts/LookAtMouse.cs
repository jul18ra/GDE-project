using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Vector2 lookDir;
    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDir = mousePos - transform.position;
        transform.up = lookDir;
    }
}
