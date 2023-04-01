using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; } = default;

	[Header("Objects")]
	[SerializeField] private GameObject gameContainer = default;
	[SerializeField] private Titlecard titlecard = default;

    [SerializeField] private Vortex vortex;

	private bool createVortex = false;
	private bool loadScene = true;

	#region Unity Methods
	private void Awake()
	{
		Instance = this;

		Vortex.colisionVortex += Vortex_colisionVortex;
        PlayerMovement.OnCollisionVortex += PlayerMovement_OnCollisionVortex;

        titlecard.Onplay += Titlecard_Onplay;
	}
    #endregion

    #region events
    private void PlayerMovement_OnCollisionVortex(PlayerMovement sender)
	{
		//Gameover plutôt
		Retry();
	}

	private void Titlecard_Onplay(BaseScreen sender)
	{
		gameContainer.SetActive(true);
	}
	#endregion

	#region Screens
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

			Debug.Log("Instantiate");
		}
    }
	#endregion

	private void OnDestroy()
	{
		Vortex.colisionVortex -= Vortex_colisionVortex;
		PlayerMovement.OnCollisionVortex -= PlayerMovement_OnCollisionVortex;
	}
}