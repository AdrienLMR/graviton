using UnityEngine;

public enum PlayerInputs
{
	Nothing,

	Horizontal,
	Vertical,
	Push,

	Horizontal2,
	Vertical2,
	Push2
}

public class PlayerInput
{
	private const string HORIZONTAL = "Horizontal";
	private const string VERTICAL = "Vertical";
	private const string PUSH = "Push";

	private const string HORIZONTAL2 = "Horizontal2";
	private const string VERTICAL2 = "Vertical2";
	private const string PUSH2 = "Push2";

	public static float GetAxis(PlayerInputs inputs)
	{
		switch (inputs)
		{
			case PlayerInputs.Horizontal:
				return Input.GetAxis(HORIZONTAL);
			case PlayerInputs.Vertical:
				return Input.GetAxis(VERTICAL);
			case PlayerInputs.Push:
				return Input.GetAxis(PUSH);

			case PlayerInputs.Horizontal2:
				return Input.GetAxis(HORIZONTAL2);
			case PlayerInputs.Vertical2:
				return Input.GetAxis(VERTICAL2);
			case PlayerInputs.Push2:
				return Input.GetAxis(PUSH2);
			default:
				return 0;
		}
	}
}
