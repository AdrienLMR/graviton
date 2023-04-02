using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ScienceScreen : BaseScreen
{
    [SerializeField] private Image image = default;
    [SerializeField] private List<Sprite> images = default;
    
    private int index = 0;

    public event BaseScreenEventHandler OnClick;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            index++;

            if (index < images.Count)
            {
                image.sprite = images[index];
            }
            else
            {
                OnClick?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
