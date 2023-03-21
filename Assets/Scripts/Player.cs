using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Wand wand;


    Camera _camera;
    void Start()
    {
        _camera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        RotateToCrosshair();
        HandleShots();
    }

    void RotateToCrosshair()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);  // TODO Change to new Input system
        mousePos.z = 0f;
        var dir = (mousePos - wand.transform.position).normalized;
        dir.z = 0;
        wand.transform.up = dir;
    }

    void HandleShots()
    {
        if (Input.GetKey(KeyCode.Mouse0)) // TODO Change to new Input system
        {
            wand.Shoot();

        }
    }
}
