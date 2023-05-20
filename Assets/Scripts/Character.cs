using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] Wand _wand;


    [SerializeField] private float _movespeed = 1f; // TODO take it form char manager
    PlayerInputActions.CharacterActions _inputActions;
    Camera _camera;
    Rigidbody2D _rb;

    void Start()
    {
        _camera = Camera.main;
        WandItem vandItem = Game.Get<InventoryManager<InventoryItem>>().Get(InventoryIds.WANDS_SLOTS_ID_PREFIX + 1) as WandItem;
        _wand.Init(vandItem);
        _rb = GetComponent<Rigidbody2D>();
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
        _rb.MovePosition(_rb.position + input * _movespeed * Time.fixedDeltaTime);
        //_rb.AddForce(input * _movespeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }


    // Update is called once per frame
    void Update()
    {
        RotateToCursor();
        HandleShooting();
    }

    void RotateToCursor()
    {
        var mousePos = _camera.ScreenToWorldPoint(_inputActions.Aim.ReadValue<Vector2>());  
        mousePos.z = 0f;
        var dir = (mousePos - _wand.transform.position).normalized;
        dir.z = 0;
        _wand.transform.up = dir;
    }

}
