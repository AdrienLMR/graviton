using UnityEngine;

[DisallowMultipleComponent]
public class PlayerAttack : MonoBehaviour
{
	[SerializeField] private GameObject vortex = default;

	[SerializeField] private float forcePushVortex = 0f;
	[SerializeField] private float distanceToSpawn = 4f;
	[SerializeField] private float coolDown = 0f;
	[SerializeField] private Animator animator;

	private float elapsedTime = default;
	public float inputAddVortex { private get; set; }

	private bool inputPressed = false;

	private void Update()
	{
		elapsedTime += Time.deltaTime;

		if (inputAddVortex > 0 && elapsedTime >= coolDown && !inputPressed)
		{
			FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_vortex_positive");

			elapsedTime = 0f;

			Vortex _vortex = CreateVortex();
			_vortex.charge = 1;
			_vortex.SetModePushed(transform.up * forcePushVortex);

			animator.SetBool("AttackRight", true);
		}
		else if (inputAddVortex < 0 && elapsedTime >= coolDown && !inputPressed)
		{
			FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_vortex_negative");

			elapsedTime = 0f;

			Vortex _vortex = CreateVortex();
			_vortex.charge = -1;
			_vortex.SetModePushed(transform.up * forcePushVortex);

			animator.SetBool("AttackLeft", true);
		}

		if (inputAddVortex == 0)
		{
			inputPressed = false;
		}else
        {
			inputPressed = true;

		}
	}

	private Vortex CreateVortex()
    {
		Vector3 positionSpawn = transform.position + transform.up * distanceToSpawn;

		return Instantiate(vortex, positionSpawn, Quaternion.identity, transform.parent).GetComponent<Vortex>();
	}
}
