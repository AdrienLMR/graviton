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

    public void FootstepSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/CHA/sound_cha_fsteps_a1");
    }


}
