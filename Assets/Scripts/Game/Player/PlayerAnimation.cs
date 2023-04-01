using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;

    public void SetModeFalseLeft()
    {
        animatorPlayer.SetBool("AttackLeft", false);
    }

    public void SetModeFalseLRight()
    {
        animatorPlayer.SetBool("AttackRight", false);
    }
}
