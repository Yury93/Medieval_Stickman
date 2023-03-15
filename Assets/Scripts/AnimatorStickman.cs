using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStickman : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string Walk;
    [SerializeField] private string StartJump;
    [SerializeField] private string Idle;

    public void SetWalk(bool walk)
    {
        animator.SetBool(Walk, walk);
    }
    public void SetJump(bool jump)
    {
        animator.SetBool(StartJump, jump);
    }
    public void SetIdle(bool idle)
    {
        animator.SetBool(Idle, idle);
    }
    public AnimatorStateInfo GetCurrentAnimatorStateLength(int layerIndex)
    {
        return animator.GetCurrentAnimatorStateInfo(layerIndex);
    }
}
