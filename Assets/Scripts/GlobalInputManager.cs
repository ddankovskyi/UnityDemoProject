
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GlobalInputManager : MonoBehaviour
{
    [SerializeField] GameObject _inventoryUI;
    [SerializeField] RectTransform _cursor;
    [SerializeField] GameObject _hudUI;

    [SerializeField] Character _character;
    bool _isInventoryShown = false;
    Camera _mainCamera;

    PlayerInputActions.GlobalActions _inputs;
    void Start()
    {
        _mainCamera = Camera.main;
        InitInputs();
        SetInventoryVisible(_isInventoryShown);
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
        _isInventoryShown = !_isInventoryShown;
        SetInventoryVisible(_isInventoryShown);
    }

    void SetInventoryVisible(bool isIvnventoryVisible)
    {
        if(isIvnventoryVisible) _character.ResetWand();
        _inventoryUI.SetActive(isIvnventoryVisible);
        _hudUI.SetActive(!isIvnventoryVisible);
    }


    void HandleCursorPos()
    {
        Vector2 cursorPos;
        var mousePos = _inputs.CursorPos.ReadValue<Vector2>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_cursor.parent.transform as RectTransform, mousePos, _mainCamera, out cursorPos);
        _cursor.anchoredPosition = cursorPos;
    }

    private void OnDestroy()
    {
        ReleaseInputs();
    }
}
