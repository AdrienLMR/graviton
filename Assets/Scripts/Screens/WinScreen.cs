using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class WinScreen : BaseScreen
{
    [SerializeField] private GameObject player1 = default;
    [SerializeField] private GameObject player2 = default;

    [SerializeField] private TextMeshProUGUI text = default;

    public event BaseScreenEventHandler OnClick;

    public void Init(int playerWin)
    {
        text.text = $"Player {playerWin} wins !";
        
        if (playerWin == 1)
        {
            player1.SetActive(true);
            player2.SetActive(false);
        }
        else
        {
            player1.SetActive(false);
            player2.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
            OnClick?.Invoke(this);
        }
    }
}
