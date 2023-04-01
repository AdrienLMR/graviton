using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public delegate void PlayerMovementDelegate(PlayerMovement sender);

[DisallowMultipleComponent]
public class PlayerMovement : Movement
{
    [Header("Player Values")]
	[SerializeField] private float speed = 0f;
	[SerializeField] private float distanceToSpawn = 4f;
	[SerializeField] private float coolDown = 0f;
	[SerializeField] private GameObject vortex = default;

	public int playerIndex = 0;
	private float elapsedTime = default;

	public Vector2 movementInput { private get; set; }
	public float inputAddVortex { private get; set; }

	public static event PlayerMovementDelegate colisionVortex;

	#region Unity Methods
	protected override void Start()
	{
		SetModeMove();
	}
    #endregion

    #region State Machine
    protected override void DoActionMove()
	{
		velocity = speed * Time.deltaTime * movementInput;

		if (CheckExceedLimitMap(transform.position - arena.position))
			SlideAgainstWall(transform.position - arena.position);

		UpdatePosition();
	}

	public void SetModeDie()
    {
		colisionVortex?.Invoke(this);
		DoAction = DoActionDie;
    }

	public void DoActionDie()
    {

    }


    #endregion

    protected override void Update()
    {
        base.Update();
		elapsedTime += Time.deltaTime;

		if (inputAddVortex > 0 && elapsedTime >= coolDown)
        {
			elapsedTime = 0f;

			Vector3 positionSpawn = transform.position + transform.right * distanceToSpawn;

			Instantiate(vortex, positionSpawn, Quaternion.identity).GetComponent<Vortex>().charge = 1;

        }else if (inputAddVortex < 0 && elapsedTime >= coolDown)
        {
			elapsedTime = 0f;

			Vector3 positionSpawn = transform.position + transform.right * distanceToSpawn;

			Instantiate(vortex, positionSpawn, Quaternion.identity).GetComponent<Vortex>().charge = -1;
		}
    }
}
