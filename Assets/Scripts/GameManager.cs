using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

	[Header("Objects")]
	[SerializeField] private GameObject gameContainer = default;
	[SerializeField] private GameObject titlecard = default;

    [SerializeField] private Vortex vortex;
	private Vortex actualVortex = default;

	private bool createVortex = false;
	private bool loadScene = true;

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
	#endregion

	#region Screens
	public void Play()
	{
		gameContainer.SetActive(true);
		titlecard.SetActive(false);
	}

	private void Retry()
    {
		if (loadScene)
        {
			SceneManager.LoadScene(0);
			loadScene = false;
        }
    }

	public void Pause()
    {

    }

	public void Resume()
    {

    }
    #endregion

    #region Vortexs
    private void Vortex_colisionVortex(Vortex sender, Vortex receiver)
    {
		Debug.Log(createVortex);

		createVortex = !createVortex;

        if (createVortex)
        {
			int chargerSender = sender.charge;
			int chargeReceiver = receiver.charge;

            Vector3 middle = (sender.transform.position + receiver.transform.position)/ 2;

			Destroy(sender.gameObject);
			Destroy(receiver.gameObject);

			Instantiate(vortex, middle, sender.transform.rotation).charge = chargeReceiver + chargerSender;

			Debug.Log("Insatantiate");
		}
    }
    
    private void PlayerMovement_colisionVortex(PlayerMovement sender)
    {
		Retry();
	}
	#endregion

	private void OnDestroy()
	{
		Vortex.colisionVortex -= Vortex_colisionVortex;
		PlayerMovement.colisionVortex -= PlayerMovement_colisionVortex;
	}
}