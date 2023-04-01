using UnityEngine;

public delegate void PlayerMovementDelegate(PlayerMovement sender);

[DisallowMultipleComponent]
public class PlayerMovement : Movement
{
    [Header("Player Values")]
	[SerializeField] private float speed = 0f;

	public int playerIndex = 0;
	public Vector2 movementInput { private get; set; }

	public static event PlayerMovementDelegate OnCollisionVortex;
	private Animator animator;

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

		UpdatePosition();
	}

	public void DoActionDie()
    {

    }
    #endregion
}
