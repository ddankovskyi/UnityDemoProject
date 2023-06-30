using Newtonsoft.Json.Schema;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Zenject;

public class Character : MonoBehaviour
{
    [SerializeField] Wand _wand;


    [SerializeField] private float _movespeed = 1f; // TODO take it form char manager
    [SerializeField] Transform _firePoint;
    PlayerInputActions.CharacterActions _inputActions;
    Camera _camera;
    CharacterController _characterController;
    SpellItemsObjectsCollector _spellItemsObjectsCollector;
    GameStateManager _gameStateManager;
    public Transform Firepoint => _firePoint;

    void Start()
    {
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _spellItemsObjectsCollector = GetComponent<SpellItemsObjectsCollector>();
        InitInputs();
    }

    [Inject]
    void Init(CharacterInventoryManager characterInventory, GameStateManager gameStateManager)
    {   
        WandItem wandItem = characterInventory.GetWandBySlotNumber(1);
        _wand.Init(wandItem, _firePoint);
        _gameStateManager = gameStateManager;
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
    }


    void ReleaseSunscriptions()
    {
        _inputActions.Interact.performed -= HandleInterractButtonPressed;
        _gameStateManager.OnGameStateChanged -= HandleGameStateChange;
    }

    void HandleGameStateChange(GameState newState)
    {
        if (_gameStateManager.IsInState(GameState.Play))
        {
            ResetWand();
        }
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
        _inputActions.Interact.performed += HandleInterractButtonPressed;
    }

 

    void HandleInterractButtonPressed(InputAction.CallbackContext context) => _spellItemsObjectsCollector.Collect();

    public void HandleShooting()
    {     
        if(_inputActions.Shoot.IsPressed() && _gameStateManager.IsInState(GameState.Play))
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
        
        var ray = _camera.ScreenPointToRay(_inputActions.Aim.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground"))) // TODO get rid of string id
        {
            var dir = (hit.point - transform.position).normalized;

            transform.forward = dir;
        }
    }

    private void OnDestroy()
    {
        ReleaseSunscriptions();
    }

}
