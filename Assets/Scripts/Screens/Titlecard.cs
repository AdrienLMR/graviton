using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Titlecard : BaseScreen
{
    [SerializeField] private Button playButton = default;

    [SerializeField] private Image imageValidatePlayer1 = default;
    [SerializeField] private Image imageValidatePlayer2 = default;

    private bool validate1 = false;
    private bool validate2 = false;

    public event BaseScreenEventHandler Onplay;

    private void Awake()
    {
        playButton.onClick.AddListener(Play);
    }

    public void StartDevice()
    {
        if (!validate1)
        {
            validate1 = true;
            imageValidatePlayer1.color = Color.green;
        }
        else
        {
            validate2 = true;
            imageValidatePlayer2.color = Color.green;

            playButton.Select();
        }
    }

    private void Play()
    {
        if (validate1 && validate2)
        {
            gameObject.SetActive(false);
            Onplay?.Invoke(this);
        }
    }
}
