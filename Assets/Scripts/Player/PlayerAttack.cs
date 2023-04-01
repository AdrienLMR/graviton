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

	private float elapsedTime = default;
	public float inputAddVortex { private get; set; }

	private void Update()
	{
		elapsedTime += Time.deltaTime;

		if (inputAddVortex > 0 && elapsedTime >= coolDown)
		{
			elapsedTime = 0f;

			Vector3 positionSpawn = transform.position + transform.up * distanceToSpawn;

			Instantiate(vortex, positionSpawn, Quaternion.identity).GetComponent<Vortex>().charge = 1;

		}
		else if (inputAddVortex < 0 && elapsedTime >= coolDown)
		{
			elapsedTime = 0f;

			Vector3 positionSpawn = transform.position + transform.up * distanceToSpawn;

			Instantiate(vortex, positionSpawn, Quaternion.identity).GetComponent<Vortex>().charge = -1;
		}
	}
}
