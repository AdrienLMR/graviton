using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScienceScreen : BaseScreen
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
