using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorStickman
{
    public Animator Animator { get; private set; }
    private const string Walk = "Walk";
    private const string StartJump = "StartJump";
    private const string Idle = "Idle";
    private const string Kick = "Kick";
    public AnimatorStickman(Animator animator)
    {
        Animator = animator;
    } 
    public void SetWalk(bool walk)
    {
        Animator.SetBool(Walk, walk);
    }
    public void SetJump(bool jump)
    {
        Animator.SetBool(StartJump, jump);
    }
    public void SetIdle(bool idle)
    {
        Animator.SetBool(Idle, idle);
    }
    public void SetKick(bool kick)
    {
        Animator.SetBool(Kick,kick);
    }
    public void GetCurrentAnimatorStateLength(int layerIndex)
    {
        
    }
}
