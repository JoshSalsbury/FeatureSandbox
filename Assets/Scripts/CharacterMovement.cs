using System;
using UnityEngine;

public abstract class CharacterMovement : MonoBehaviour
{
    
    public enum MovementState
    {
        Idle,
        Walking,
    }
    
    public class OnStateChangedArgs : EventArgs
    {
        public MovementState State;
    }

    public event EventHandler<OnStateChangedArgs> OnStateChanged;

    private MovementState _state = MovementState.Idle;

    public MovementState State
    {
        get => _state;
        protected set
        {
            if (_state == value)
            {
                return;
            }
            _state = value;
            OnStateChanged?.Invoke(this, new OnStateChangedArgs() { State = value });
        }
    }

    public bool IsIdle()
    {
        return _state == MovementState.Idle;
    }

    public bool IsWalking()
    {
        return _state == MovementState.Walking;
    }

    protected Vector2 GetVector2MovementTransform(float moveSpeed, Vector2 inputDirection)
    {
        return moveSpeed * Time.deltaTime * inputDirection;
    }

    protected Vector3 GetVector3MovementTransform(float moveSpeed, Vector2 inputDirection)
    {
        return GetVector2MovementTransform(moveSpeed, inputDirection).EmbedToVector3();
    }

}
