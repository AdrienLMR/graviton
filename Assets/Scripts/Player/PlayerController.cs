using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerInput playerInput = default;

	private PlayerMovement playerMovement = default;

    private void Awake()
    {
		var players = FindObjectsOfType<PlayerMovement>();
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
		playerMovement.movementInput = ctx.ReadValue<Vector2>();
	}
}
