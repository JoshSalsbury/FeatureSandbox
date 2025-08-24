using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameInput gameInput;

    private Animator _animator;
    
    private const string PLAYER_ANIMATOR_MOVE_X = "moveX";
    private const string PLAYER_ANIMATOR_MOVE_Y = "moveY";
    private const string PLAYER_ANIMATOR_IS_WALKING = "isWalking";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        playerMovement.OnStateChanged += PlayerMovement_OnStateChanged;
        playerMovement.OnNonZeroInputMovement += PlayerMovement_OnNonZeroInputMovement;
    }

    private void PlayerMovement_OnStateChanged(object sender, CharacterMovement.OnStateChangedArgs args)
    {
        _animator.SetBool(PLAYER_ANIMATOR_IS_WALKING, playerMovement.IsWalking());
    }

    private void PlayerMovement_OnNonZeroInputMovement(object sender, PlayerMovement.OnNonZeroInputMovementArgs args)
    {
        _animator.SetFloat(PLAYER_ANIMATOR_MOVE_X, args.InputMovement.x);
        _animator.SetFloat(PLAYER_ANIMATOR_MOVE_Y, args.InputMovement.y);
    }
    
}
