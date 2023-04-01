using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerMovement : Movement
{
    [Header("Player Values")]
	[SerializeField] private float speed = 0f;

	public int playerIndex = 0;

	public Vector2 movementInput { private get; set; }

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
	#endregion
}
