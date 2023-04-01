using UnityEngine;
using UnityEngine.InputSystem;

public delegate void PlayerControllerEventHandler(PlayerController sender);

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerInput playerInput = default;

	private PlayerMovement playerMovement = default;

	public static event PlayerControllerEventHandler OnPauseGame;

	private void Awake()
    {
		PlayerMovement[] players = FindObjectsOfType<PlayerMovement>(true);
		int index = playerInput.playerIndex;

        for (int i = 0; i < players.Length; i++)
        {
			if (index == players[i].playerIndex)
            {
				playerMovement = players[i];
				break;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
	{
		if (playerMovement != null)
			playerMovement.movementInput = ctx.ReadValue<Vector2>();
	}

	public void OnRotate(InputAction.CallbackContext ctx)
    {
		if (playerMovement != null)
			playerMovement.rotateInput = ctx.ReadValue<Vector2>();
    }

	public void OnAddVortex(InputAction.CallbackContext ctx)
	{
		if (playerMovement != null)
			playerMovement.gameObject.GetComponent<PlayerAttack>().inputAddVortex = ctx.ReadValue<float>();
    }

	public void OnPush(InputAction.CallbackContext ctx)
    {
		if (playerMovement != null)
			playerMovement.gameObject.GetComponentInChildren<PlayerPush>().Push();
    }

	public void OnPause(InputAction.CallbackContext ctx)
    {
		OnPauseGame?.Invoke(this);
    }
}
