
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class GlobalInputManager : MonoBehaviour
{

    PlayerInputActions.GlobalActions _inputs;
    GameStateManager _gameStateManager;

    void Start()
    {
        InitInputs();
        _gameStateManager = Game.Get<GameStateManager>();

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



    private void OnDestroy()
    {
        ReleaseInputs();
    }
}
