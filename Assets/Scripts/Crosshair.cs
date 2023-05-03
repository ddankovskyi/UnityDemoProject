using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
   
    [SerializeField] bool _hideCoursour;

    Camera _camera;
    float _initZPos;
    PlayerInputActions.GeneralActions _inputActions;
    void InitInput()
    {
        _inputActions = new PlayerInputActions().General;
        _inputActions.Enable();
    }
    void Start()
    {
        _camera = Camera.main;
        _initZPos = transform.position.z;
        Cursor.visible = !_hideCoursour;
        InitInput();
    }

    void Update()
    {
        var mousePos = _camera.ScreenToWorldPoint(_inputActions.CursorPos.ReadValue<Vector2>());
        mousePos.z = _initZPos;
        transform.position = mousePos;
    }
}
