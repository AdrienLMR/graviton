using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Inputs{
	Nothing,
	Horizontal,
	Vertical,
	Horizontal2,
	Vertical2
}

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
	private const string HORIZONTAL = "Horizontal";
	private const string HORIZONTAL2 = "Horizontal2";
	private const string VERTICAL = "Vertical";
	private const string VERTICAL2 = "Vertical2";

	[Header("Objects")]
	[SerializeField] private Transform arena = default;

    [Header("Values")]
	[SerializeField] private float speed = 0f;
	[SerializeField] private Inputs horizontal = Inputs.Nothing;
	[SerializeField] private Inputs vertical = Inputs.Nothing;

	private Vector3 velocity = Vector3.zero;

    private Action DoAction;

	#region Unity Methods
	public void Start()
	{
		SetModePlay();
	}

	public void Update()
	{
		DoAction();
	}
	#endregion

	#region State Machine		
	public void SetModeVoid()
	{
		DoAction = DoActionVoid;
	}

	public void SetModePlay()
	{
		DoAction = DoActionPlay;
	}

	private void DoActionVoid() { }

	private void DoActionPlay()
	{
		SetVelocity();

		if (CheckExceedLimitMap(transform.position - arena.position))
			SlideAgainstWall(transform.position - arena.position);

		UpdatePosition();
	}
    #endregion

    #region velocity
    private void SetVelocity()
    {
		velocity = speed * Time.deltaTime * new Vector3(GetAxis(horizontal), GetAxis(vertical)).normalized;
	}

	private float GetAxis(Inputs inputs)
	{
		switch (inputs)
		{
			case Inputs.Horizontal:
				return Input.GetAxis(HORIZONTAL);
			case Inputs.Horizontal2:
				return Input.GetAxis(HORIZONTAL2);
			case Inputs.Vertical:
				return Input.GetAxis(VERTICAL);
			case Inputs.Vertical2:
				return Input.GetAxis(VERTICAL2);
			default:
				return 0;
		}
	}
    #endregion

    #region Exceed Limit
    private bool CheckExceedLimitMap(Vector3 localPosition)
		=> (localPosition + velocity).magnitude > arena.localScale.x / 2;

	private void SlideAgainstWall(Vector3 localPosition)
    {
		velocity = ((localPosition + velocity).normalized * arena.localScale.x / 2) - localPosition;
    }
	#endregion

	private void UpdatePosition()
    {
		transform.position += velocity;
	}
}
