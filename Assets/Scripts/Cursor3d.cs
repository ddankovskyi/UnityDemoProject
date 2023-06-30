using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Zenject;

public class Cursor3d : MonoBehaviour
{

    PlayerInputActions.CharacterActions _inputs;
    Camera _camera;
    bool _isActive;
    Renderer _renderer;
    Transform _firepoint;

    GameStateManager _gameStateManager;

    //Transform _firePointTransform;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        InitInputs();
        _camera = Camera.main;
        Cursor.visible = false;
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        _isActive = _gameStateManager.IsInState(GameState.Play);
        //_firePointTransform = Game.Get<CharacterManager>().CurrentCharacter.Firepoint;
    }

    [Inject]
    void InitFirePoint(CharacterManager characterManager, GameStateManager gameStateManager)
    {
        _firepoint = characterManager.CurrentCharacter?.Firepoint;
        _gameStateManager = gameStateManager;
    }

    private void OnDestroy()
    {
        _gameStateManager.OnGameStateChanged -= HandleGameStateChange;
    }

    void HandleGameStateChange(GameState state)
    {
        _isActive = state == GameState.Play;
        _renderer.enabled = _isActive;
    }

    void InitInputs()
    {
        _inputs = new PlayerInputActions().Character;
        _inputs.Enable();
    }

    void HandleCursorPos()
    {
        var ray = _camera.ScreenPointToRay(_inputs.Aim.ReadValue<Vector2>());
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Ground"))) // TODO get rid of string id
        {
            Vector3 pos = hit.point;
            pos.y = _firepoint.position.y;
            transform.position = pos;
            Debug.DrawLine(_camera.transform.position, hit.point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            HandleCursorPos();
            transform.forward = _camera.transform.forward;
        }
    }
}
