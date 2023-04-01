using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

	[Header("Objects")]
	[SerializeField] private GameObject gameContainer = default;
	[SerializeField] private Titlecard titlecard = default;
    [SerializeField] private ScienceScreen scienceScreen = default;
    [SerializeField] private PauseScreen pauseScreen = default;

    [SerializeField] private Vortex vortex;

	private bool createVortex = false;
	private bool loadScene = true;

	#region Unity Methods
	private void Awake()
	{
		Instance = this;

        Vortex.OncollisionVortex += Vortex_OncollisionVortex;
        PlayerMovement.OnCollisionVortex += PlayerMovement_OnCollisionVortex;
        PlayerController.OnPauseGame += PlayerController_OnPauseGame;

        titlecard.Onplay += Titlecard_Onplay;
        scienceScreen.OnClick += ScienceScreen_OnClick;
        pauseScreen.OnClick += PauseScreen_OnClick;
	}
    #endregion

    #region Events
    private void PlayerMovement_OnCollisionVortex(PlayerMovement sender)
	{
		//Gameover plut�t
		Retry();
	}

	private void Vortex_OncollisionVortex(Vortex sender, Vortex receiver)
	{
		Debug.Log(createVortex);

		createVortex = !createVortex;

		if (createVortex)
		{
			int chargerSender = sender.charge;
			int chargeReceiver = receiver.charge;

			Vector3 middle = (sender.transform.position + receiver.transform.position) / 2;

			Destroy(sender.gameObject);
			Destroy(receiver.gameObject);

			Instantiate(vortex, middle, sender.transform.rotation, gameContainer.transform).charge = chargeReceiver + chargerSender;

			Debug.Log("Instantiate");
		}
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
	#endregion

	#region Screens
	private void Retry()
    {
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