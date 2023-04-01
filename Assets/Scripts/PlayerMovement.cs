using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void _OnCollisionVortex(PlayerMovement sender);

[DisallowMultipleComponent]
public class PlayerMovement : Movement
{
    [Header("Player Values")]
	[SerializeField] private float speed = 0f;
	[SerializeField] private PlayerInputs horizontalInput = PlayerInputs.Nothing;
	[SerializeField] private PlayerInputs verticalInput = PlayerInputs.Nothing;
	[SerializeField] private float coolDownVortex = 0.5f;
	[SerializeField] private GameObject vortex;
	[SerializeField] private float distanceAddVortex;

	private float elapsedTimeCoolDown = default;

	public static event _OnCollisionVortex colisionVortex;

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

	public void SetModeDie()
    {
		DoAction = DoActionDie;
		colisionVortex?.Invoke(this);
		Destroy(gameObject);
    }

	private void DoActionDie()
    {

    }


	#endregion

	#region velocity inputs
	private void SetVelocityInput()
    {
        velocity = speed * Time.deltaTime * new Vector3(PlayerInput.GetAxis(horizontalInput), PlayerInput.GetAxis(verticalInput)).normalized;
    }
    #endregion

    protected override void Update()
    {
        base.Update();
		elapsedTimeCoolDown += Time.deltaTime;
		int charge = 0;

		if (Input.GetMouseButtonDown(0) && elapsedTimeCoolDown >= coolDownVortex)
        {
			Debug.Log("AddVortex");
			elapsedTimeCoolDown = 0f;

			Vector3 positonSpawn = transform.position + transform.right * distanceAddVortex;
			charge = 1;

			Instantiate(vortex, positonSpawn, Quaternion.identity).GetComponent<Vortex>().charge = charge;

		}else if (Input.GetMouseButtonDown(1) && elapsedTimeCoolDown >= coolDownVortex)
        {
			Debug.Log("AddVortex");
			elapsedTimeCoolDown = 0f;

			Vector3 positonSpawn = transform.position + transform.right * distanceAddVortex;
			charge = -1;

			Instantiate(vortex, positonSpawn, Quaternion.identity).GetComponent<Vortex>().charge = charge;
		}
    }
}
