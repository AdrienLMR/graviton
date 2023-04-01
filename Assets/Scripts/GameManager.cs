using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

    [Header("Objects")]
    [SerializeField] private Vortex vortex;
	private Vortex actualVortex = default;

	[SerializeField] private GameObject playerInput = default;

	private bool createVortex = false;

	#region Unity Methods
	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
        Vortex.colisionVortex += Vortex_colisionVortex;
    }

    private void Vortex_colisionVortex(Vortex sender, Vortex receiver)
    {
		createVortex = !createVortex;

        if (createVortex)
        {
			int chargerSender = sender.charge;
			int chargeReceiver = receiver.charge;

            Vector3 middle = (sender.transform.position + receiver.transform.position)/ 2;

			Instantiate(vortex, middle, sender.transform.rotation).charge = chargeReceiver + chargerSender;

			Destroy(sender.gameObject);
			Destroy(receiver.gameObject);
		}
    }
	#endregion

	public void CollisionVortex(Vortex vortex)
    {
		
    }
}