using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimVortex : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetFusionPlus()
    {
        animator.SetBool("FusionPlus", false);
    }

    public void SetFusionMoins()
    {
        animator.SetBool("FusionMoins", false);
    }
}
