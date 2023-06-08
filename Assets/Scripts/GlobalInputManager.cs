
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GlobalInputManager : MonoBehaviour
{
    [SerializeField] RectTransform _cursor;
    [SerializeField] Character _character;
    Camera _mainCamera;

    PlayerInputActions.GlobalActions _inputs;
    GameStateManager _gameStateManager;

    void Start()
    {
        _mainCamera = Camera.main;
        InitInputs();
        _gameStateManager = Game.Get<GameStateManager>();

    }

    void Update()
    {
        HandleCursorPos();
    }

    void InitInputs()
    {
        _inputs = new PlayerInputActions().Global;
        _inputs.Enable();
        _inputs.InventoryBtn.performed += HandleInentoryBtnInput;
    }

    void ReleaseInputs()
    {
        _inputs.InventoryBtn.performed -= HandleInentoryBtnInput;
    }

    void HandleInentoryBtnInput(InputAction.CallbackContext context)
    {
        if(_gameStateManager.IsInState(GameState.Inventory))
        {
            _gameStateManager.TrySetState(GameState.Play);
        } else
        {
            _gameStateManager.TrySetState(GameState.Inventory);
        }
    }

    void HandleCursorPos()
    {
        if (_cursor.gameObject.activeSelf)
        {
            Vector2 cursorPos;
            var mousePos = _inputs.CursorPos.ReadValue<Vector2>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_cursor.parent.transform as RectTransform, mousePos, _mainCamera, out cursorPos);
            _cursor.anchoredPosition = cursorPos;
        }
    }

    private void OnDestroy()
    {
        ReleaseInputs();
    }
}
