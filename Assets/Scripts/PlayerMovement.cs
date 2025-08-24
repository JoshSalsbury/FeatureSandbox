using System;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{

    public class OnNonZeroInputMovementArgs : EventArgs
    {
        public Vector2 InputMovement;
    }

    public event EventHandler<OnNonZeroInputMovementArgs> OnNonZeroInputMovement;
    
    [SerializeField] private GameInput gameInput;
    
    private Rigidbody2D _rigidbody2D;
    
    private const float PLAYER_MOVEMENT_DEADZONE = 0.5f;
    private const float PLAYER_MOVE_SPEED = 7f;
    private const float PLAYER_SIZE = 0.7f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 inputDirection = gameInput.GetMovementVectorNormalized();
        UpdateMovementState(inputDirection);
        _rigidbody2D.linearVelocity = PLAYER_MOVE_SPEED * inputDirection;
    }

    private void UpdateMovementState(Vector2 inputDirection)
    {
        if (inputDirection.IsZeroVector())
        {
            State = MovementState.Idle;
            return;
        }
        State = MovementState.Walking;
        OnNonZeroInputMovement?.Invoke(this, new OnNonZeroInputMovementArgs { InputMovement = inputDirection });
    }
    
}
