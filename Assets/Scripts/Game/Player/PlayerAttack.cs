using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private GameObject vortex = default;

	[SerializeField] private float distanceToSpawn = 4f;
	[SerializeField] private float coolDown = 0f;
	[SerializeField] private Animator animator;

	private float elapsedTime = default;
	public float inputAddVortex { private get; set; }

	private void Update()
	{
		elapsedTime += Time.deltaTime;

		if (inputAddVortex > 0 && elapsedTime >= coolDown)
		{
			FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_vortex_positive");
			elapsedTime = 0f;

			Vector3 positionSpawn = transform.position + transform.up * distanceToSpawn;

			Vortex _vortex = Instantiate(vortex, positionSpawn, Quaternion.identity).GetComponent<Vortex>();
			_vortex.charge = 1;
			animator.SetBool("AttackRight", true);

		}
		else if (inputAddVortex < 0 && elapsedTime >= coolDown)
		{
			FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_vortex_negative");
			elapsedTime = 0f;

			Vector3 positionSpawn = transform.position + transform.up * distanceToSpawn;

			Vortex _vortex = Instantiate(vortex, positionSpawn, Quaternion.identity).GetComponent<Vortex>();
			_vortex.charge = -1;
			animator.SetBool("AttackLeft", true);


		}

		//animator.SetBool("AttackRight", false);
		//animator.SetBool("AttackLeft", false);

	}
}
