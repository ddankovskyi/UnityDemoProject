using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Cursor2d : MonoBehaviour
{

    PlayerInputActions.GlobalActions _inputs;
    Camera _camera;
    bool _isActive;
    [SerializeField] Image _image;
    [Inject] GameStateManager _gameStateManager;
    RectTransform _rectTransform;

    void Start()
    {
        //_image = GetComponent<Image>();
        InitInputs();
        _camera = Camera.main;
        Cursor.visible = false;
        _gameStateManager.OnGameStateChanged += HandleGameStateChange;
        _isActive = _gameStateManager.IsInState(GameState.Inventory);
        _image.enabled = _isActive;
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        _gameStateManager.OnGameStateChanged -= HandleGameStateChange;
    }

    void HandleGameStateChange(GameState state)
    {
        _isActive = state == GameState.Inventory;
        _image.enabled = _isActive;
    }

    void InitInputs()
    {
        _inputs = new PlayerInputActions().Global;
        _inputs.Enable();
    }

    void HandleCursorPos()
    {
        Vector2 cursorPos;
        var mousePos = _inputs.CursorPos.ReadValue<Vector2>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.transform as RectTransform, mousePos, _camera, out cursorPos);
        _rectTransform.anchoredPosition = cursorPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive) HandleCursorPos();
    }
}
