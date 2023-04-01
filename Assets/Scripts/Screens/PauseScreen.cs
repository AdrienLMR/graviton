using UnityEngine;

[DisallowMultipleComponent]
public class PauseScreen : BaseScreen
{
    public event BaseScreenEventHandler OnClick;

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
            OnClick?.Invoke(this);
        }
    }
}
