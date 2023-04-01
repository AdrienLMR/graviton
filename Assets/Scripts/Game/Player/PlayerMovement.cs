using UnityEngine;

public delegate void PlayerMovementDelegate(PlayerMovement sender);

[DisallowMultipleComponent]
public class PlayerMovement : Movement
{
    [Header("Player Values")]
    [SerializeField] private float speed = 0f;

    [SerializeField] private string boolAnimator = "Run";

    public int playerIndex = 0;
    public Vector2 movementInput { private get; set; }
    public Vector2 rotateInput { private get; set; }

    public static event PlayerMovementDelegate OnCollisionVortex;
    [SerializeField] private Animator animator;

    #region Unity Methods
    protected override void Start()
    {
        SetModeMove();
    }
    #endregion

    #region State Machine
    public void SetModeDie()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_death_a1");
        OnCollisionVortex?.Invoke(this);
        DoAction = DoActionDie;
    }

    protected override void DoActionMove()
    {
        velocity = speed * Time.deltaTime * movementInput;

        if (CheckExceedLimitMap(transform.position - arena.position))
            SlideAgainstWall(transform.position - arena.position);

        if (movementInput.magnitude == 0f)
        {
            animator.SetBool(boolAnimator, false);
        }
        else
        {
            animator.SetBool(boolAnimator, true);
        }

        UpdatePosition();
    }

    public void DoActionDie()
    {

    }
    #endregion

    private Vector2 test = Vector2.zero;

    protected override void UpdatePosition()
    {
        base.UpdatePosition();

        if (rotateInput != Vector2.zero && rotateInput.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateInput);
    }
}
