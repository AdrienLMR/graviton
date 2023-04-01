using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : Movement
{
    [Header("Player Values")]
	[SerializeField] private float speed = 0f;
	[SerializeField] private PlayerInputs horizontalInput = PlayerInputs.Nothing;
	[SerializeField] private PlayerInputs verticalInput = PlayerInputs.Nothing;

	#region Unity Methods
	protected override void Start()
	{
		SetModeMove();
	}
    #endregion

    #region State Machine
    protected override void DoActionMove()
	{
		SetVelocityInput();

		if (CheckExceedLimitMap(transform.position - arena.position))
			SlideAgainstWall(transform.position - arena.position);

		UpdatePosition();
	}
	#endregion

	#region velocity inputs
	private void SetVelocityInput()
    {
        velocity = speed * Time.deltaTime * new Vector3(PlayerInput.GetAxis(horizontalInput), PlayerInput.GetAxis(verticalInput)).normalized;
    }
    #endregion
}
