using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

	[Header("Objects")]
	[SerializeField] private Transform currentCamera = default;
	[SerializeField] private GameObject gameContainer = default;
	[SerializeField] private Titlecard titlecard = default;
    [SerializeField] private ScienceScreen scienceScreen = default;
    [SerializeField] private PauseScreen pauseScreen = default;
    [SerializeField] private WinScreen winScreen = default;

    [SerializeField] private Vortex vortex;

	[Header("Values")]
	[SerializeField] private float screenShakeTime = 0.5f;
	[SerializeField] private float screenShakeStrength = 0.5f;
	[SerializeField] private int screenShakeVibrato = 10;

	private bool createVortex = false;
	private bool loadScene = true;

	#region Unity Methods
	private void Awake()
	{
		Instance = this;

        Vortex.OncollisionVortex += Vortex_OncollisionVortex;
        PlayerMovement.OnCollisionVortex += PlayerMovement_OnCollisionVortex;
        PlayerController.OnPauseGame += PlayerController_OnPauseGame;
        Explosion.OnExplode += Explosion_OnExplode;

        titlecard.Onplay += Titlecard_Onplay;
        scienceScreen.OnClick += ScienceScreen_OnClick;
        pauseScreen.OnClick += PauseScreen_OnClick;
        winScreen.OnClick += WinScreen_OnClick;
	}
    #endregion

    #region Events
    private void PlayerMovement_OnCollisionVortex(PlayerMovement sender)
	{
		gameContainer.SetActive(false);
		winScreen.gameObject.SetActive(true);

		winScreen.Init(sender.playerIndex == 1 ? 1 : 2);
	}

	private void Vortex_OncollisionVortex(Vortex sender, Vortex receiver)
	{
		createVortex = !createVortex;

		if (createVortex)
		{
			int chargerSender = sender.charge;
			int chargeReceiver = receiver.charge;

			Vector3 middle = (sender.transform.position + receiver.transform.position) / 2;

			Destroy(sender.gameObject);
			Destroy(receiver.gameObject);

			int charge = chargeReceiver + chargerSender;

			int maxCharge = 0;
			int absChargeReceiver = Mathf.Abs(chargeReceiver);
			int absChargerSender = Mathf.Abs(chargerSender);
			int absCharge = Mathf.Abs(charge);

			if (absChargeReceiver > absChargerSender)
			{
				maxCharge = absChargeReceiver;
			}
			else if (absChargeReceiver <= absChargerSender)
			{
				maxCharge = absChargerSender;
			}

			if (absCharge > maxCharge && charge != 0)
			{
				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sound_sfx_vortex_augment");
			}
			else if (absCharge <= maxCharge && charge != 0)
			{
				FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/sound_sfx_vortex_reduce");
			}

			Vortex _vortex = Instantiate(vortex, middle, sender.transform.rotation, gameContainer.transform).GetComponent<Vortex>();
			_vortex.charge = charge;

			if (charge > 0 && maxCharge < absCharge)
			{
				_vortex.animator.SetBool("FusionPlus", true);
			}
			else if (charge < 0 && maxCharge < absCharge)
			{
				_vortex.animator.SetBool("FusionMoins", true);
			}
		}
	}

	private void Explosion_OnExplode(Explosion sender)
	{
		currentCamera.DOShakePosition(screenShakeTime, screenShakeStrength, screenShakeVibrato);
	}

	private void Titlecard_Onplay(BaseScreen sender)
	{
		scienceScreen.gameObject.SetActive(true);
	}

	private void ScienceScreen_OnClick(BaseScreen sender)
	{
		gameContainer.SetActive(true);
	}

	private void PlayerController_OnPauseGame(PlayerController sender)
	{
		pauseScreen.gameObject.SetActive(true);
		gameContainer.SetActive(false);
		Time.timeScale = 0;
	}

	private void PauseScreen_OnClick(BaseScreen sender)
	{
		gameContainer.SetActive(true);
		Time.timeScale = 1;
	}

	private void WinScreen_OnClick(BaseScreen sender)
	{
		//Gameover plut�t
		if (loadScene)
		{
			Debug.Log("StopMusic");
			//musicSystem.StopMusic();
			SceneManager.LoadScene(0);
			loadScene = false;
		}
	}
	#endregion

	private void OnDestroy()
	{
		Vortex.OncollisionVortex -= Vortex_OncollisionVortex;
		PlayerMovement.OnCollisionVortex -= PlayerMovement_OnCollisionVortex;
	}
}