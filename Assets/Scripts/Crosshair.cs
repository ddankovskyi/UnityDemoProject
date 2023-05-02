using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
   
    [SerializeField] bool _hideCoursour;

    Camera _camera;
    float _initZPos;

    void Start()
    {
        _camera = Camera.main;
        _initZPos = transform.position.z;
        Cursor.visible = !_hideCoursour;
    }

    void Update()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);  // TODO Change to new Input system
        mousePos.z = _initZPos;
        transform.position = mousePos;
    }
}
