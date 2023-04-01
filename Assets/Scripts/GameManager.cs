using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

	private Vortex actualVortex = default;
	[SerializeField] private Vortex vortex;

    //[Header("Objects")]

    //[Header("Values")]

    private Action DoAction;

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

    public void Init()
	{

	}

	private void Update()
	{

	}

	public void GameLoop()
	{

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

	}

	#endregion
	public void CollisionVortex(Vortex vortex)
    {
		
    }

	#region Events
	#endregion
}