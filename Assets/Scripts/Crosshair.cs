using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);  // TODO Change to new Input system
        mousePos.z = 0f;
        transform.position = mousePos;
    }
}
