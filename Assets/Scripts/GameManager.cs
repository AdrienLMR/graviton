using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

	[SerializeField] private Vortex vortex;

    //[Header("Objects")]

    //[Header("Values")]

    private Action DoAction;

	private bool createVortex = false;
	private bool sceneLoad = true;

	#region Unity Methods
	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
        Vortex.colisionVortex += Vortex_colisionVortex;
        PlayerMovement.colisionVortex += PlayerMovement_colisionVortex;
	}

    private void PlayerMovement_colisionVortex(PlayerMovement sender)
    {
		Retry();
	}

	private void Retry()
    {
		if (sceneLoad)
		{
			SceneManager.LoadScene(0);
			sceneLoad = false;
		}
    }

    private void Vortex_colisionVortex(Vortex sender, Vortex receiver)
    {
		createVortex = !createVortex;

        if (createVortex)
        {
			Debug.Log("CreateVortex");

			int chargerSender = sender.charge;
			int chargeReceiver = receiver.charge;

            Vector3 middle = (sender.transform.position + receiver.transform.position)/ 2;

			Destroy(sender.gameObject);
			Destroy(receiver.gameObject);

			Vortex _vortex = Instantiate(vortex, middle, sender.transform.rotation).GetComponent<Vortex>();
			_vortex.charge = chargeReceiver + chargerSender;
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

    private void OnDestroy()
    {
		Vortex.colisionVortex -= Vortex_colisionVortex;
		PlayerMovement.colisionVortex -= PlayerMovement_colisionVortex;
	}
}