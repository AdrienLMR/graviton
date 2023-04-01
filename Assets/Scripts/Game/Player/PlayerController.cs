using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerInput playerInput = default;

	private PlayerMovement playerMovement = default;

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
}
