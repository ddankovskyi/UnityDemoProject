using Newtonsoft.Json.Schema;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] Wand _wand;


    [SerializeField] private float _movespeed = 1f; // TODO take it form char manager
    PlayerInputActions.CharacterActions _inputActions;
    Camera _camera;
    CharacterController _characterController;

    void Start()
    {
        _camera = Camera.main;
        WandItem wandItem = Game.Get<InventoryManager<InventoryItem>>().Get(InventoryIds.WANDS_SLOTS_ID_PREFIX + 1) as WandItem;
        _wand.Init(wandItem);
        _characterController = GetComponent<CharacterController>();
        InitInputs();
    }

    public void ResetWand()
    {
        Debug.Log("Reset wand");
        _wand?.ResetState();
    }


    void InitInputs()
    {
        _inputActions = new PlayerInputActions().Character;
        _inputActions.Enable();
    }

    public void HandleShooting()
    {     
        if(_inputActions.Shoot.IsPressed())
            _wand.Shoot();   
    }

    public void MovePlayer()
    {
        Vector2 input = _inputActions.Move.ReadValue<Vector2>();
        Vector3 input3 = new Vector3(input.x, 0, input.y);
        input3 = Quaternion.AngleAxis(_camera.transform.rotation.eulerAngles.y, Vector3.up) * input3;
        if (input == Vector2.zero) return;
        _characterController.Move(input3 * _movespeed * Time.deltaTime);
       
    }


    // Update is called once per frame
    void Update()
    {
        RotateToCursor();
        MovePlayer();
        HandleShooting();
    }

    void RotateToCursor()
    {
        //var mousePos = _camera.ScreenToWorldPoint(_inputActions.Aim.ReadValue<Vector2>());  // TODO Change to new Input system
        //mousePos.z = 0f;

        var ray = _camera.ScreenPointToRay(_inputActions.Aim.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground"))) // TODO get rid of string id
        {
            var dir = (hit.point - transform.position).normalized;

            transform.forward = dir;
            //transform.LookAt(hit.point);
        }
        //var dir = (mousePos - wand.transform.position).normalized;
        //dir.z = 0;
        //wand.transform.up = dir;
    }

}
