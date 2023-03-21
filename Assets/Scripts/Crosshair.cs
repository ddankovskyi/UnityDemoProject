using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);  // TODO Change to new Input system
        mousePos.z = 0f;
        transform.position = mousePos;
    }
}
