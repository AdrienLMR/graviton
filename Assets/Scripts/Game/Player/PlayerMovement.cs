using UnityEngine;
using DG.Tweening;

public delegate void PlayerMovementDelegate(PlayerMovement sender);

[DisallowMultipleComponent]
public class PlayerMovement : Movement
{
    [Header("Player Values")]
    [SerializeField] private float speed = 0f;

    [SerializeField] private string boolAnimator = "Run";
    [SerializeField] private float durationAnimDeath;
    [SerializeField] private float speedRotation = 20f;

    public int playerIndex = 0;
    public Vector2 movementInput { private get; set; }
    public Vector2 rotateInput { private get; set; }

    public static event PlayerMovementDelegate OnCollisionVortex;
    [SerializeField] private Animator animator;

    private Vector3 vortexPositionDeath;
    private Vector3 actualPosition;

    private float elapsedTimeAnimDeath = 0f;

    #region Unity Methods
    protected void Start()
    {
        SetModeMove();
    }
    #endregion

    #region State Machine
    public void SetModeDie(Vector3 vortexPosition)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_death_a1");
        DoAction = DoActionDie;

        vortexPositionDeath = vortexPosition;
        actualPosition = transform.position;
        transform.DOScale(Vector3.zero, durationAnimDeath).OnComplete(Die);
    }

    public void DoActionDie()
    {
        elapsedTimeAnimDeath += Time.deltaTime;
        float ratio = elapsedTimeAnimDeath / durationAnimDeath;

        transform.rotation = transform.rotation * Quaternion.AngleAxis(speedRotation * Time.deltaTime, Vector3.forward);
        transform.position = Vector3.Lerp(actualPosition, vortexPositionDeath, ratio);
    }

    private void Die()
    {
        OnCollisionVortex?.Invoke(this);
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

  
    #endregion

    protected override void UpdatePosition()
    {
        base.UpdatePosition();

        if (rotateInput != Vector2.zero && rotateInput.magnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateInput);
    }
}
