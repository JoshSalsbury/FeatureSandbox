using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public event EventHandler OnSaveRequested;
    
    public static GameInput Instance { get; private set; }

    private PlayerInputActions _playerInputActions;
    
    private void Awake()
    {
        Instance = this;
        InitializePlayerInputActions();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return GetMovementVector().normalized;
    }
    
    public Vector2 GetMovementVector()
    {
        return _playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    private void InitializePlayerInputActions()
    {
        _playerInputActions = new ();
        _playerInputActions.Enable();
        _playerInputActions.Player.Save.performed += GameInput_SaveRequested;
    }

    private void GameInput_SaveRequested(InputAction.CallbackContext context)
    {
        OnSaveRequested?.Invoke(this, EventArgs.Empty);
        Debug.Log("GameInput_SaveRequested");
    }
    
}
