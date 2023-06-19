using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Cursor3d : MonoBehaviour
{

    PlayerInputActions.CharacterActions _inputs;
    Camera _camera;
    bool _isActive;
    Renderer _renderer;
    GameStateManager _gameStateManager;

    //Transform _firePointTransform;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        InitInputs();
        _camera = Camera.main;
        Cursor.visible = false;
        _gameStateManager = Game.Get<GameStateManager>();
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        _isActive = _gameStateManager.IsInState(GameState.Play);
        //_firePointTransform = Game.Get<CharacterManager>().CurrentCharacter.Firepoint;
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
            pos.y = Game.Get<CharacterManager>().CurrentCharacter.Firepoint.position.y;
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
