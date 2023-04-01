using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Movement : MonoBehaviour
{
	[Header("Movement Objects")]
	[SerializeField] protected Transform arena = default;

	[Header("Movement Values")]
	[SerializeField] protected float friction = 0.98f;

	protected Vector3 velocity = Vector3.zero;

	private Action DoAction;

	#region Unity Methods
	protected virtual void Start()
	{
		SetModeVoid();
	}

	protected virtual void Update()
	{
		DoAction();
	}
	#endregion

	#region State Machine		
	public void SetModeVoid()
	{
		DoAction = DoActionVoid;
	}

	public void SetModeMove()
	{
		DoAction = DoActionMove;
	}

	public void SetModePushed(Vector3 velocity)
    {
		DoAction = DoActionPushed;
		this.velocity = velocity;
	}

	protected virtual void DoActionVoid() { }

	protected virtual void DoActionMove()
    {
		UpdatePosition();
    }
	
	protected virtual void DoActionPushed()
    {
		velocity *= friction;

		if (CheckExceedLimitMap(transform.position - arena.position))
			SlideAgainstWall(transform.position - arena.position);

		UpdatePosition();

		if (velocity == Vector3.zero)
			SetModeMove();
    }
	#endregion

	#region Exceed Limit
	protected bool CheckExceedLimitMap(Vector3 localPosition)
		=> (localPosition + velocity).magnitude > arena.localScale.x / 2;

	protected void SlideAgainstWall(Vector3 localPosition)
	{
		velocity = ((localPosition + velocity).normalized * arena.localScale.x / 2) - localPosition;
	}
	#endregion

	protected void UpdatePosition()
	{
		transform.position += velocity;
	}
}