using UnityEngine;

[DisallowMultipleComponent]
public class PauseScreen : BaseScreen
{
    public event BaseScreenEventHandler OnClick;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            OnClick?.Invoke(this);
        }
    }
}
