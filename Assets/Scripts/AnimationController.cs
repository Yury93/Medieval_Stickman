using System;
using System.Collections;
using UnityEngine;


public enum PersonState
{
    Walk,
    Jump,
    Idle,
    Kick_Idle,
    Magic_Idle,
    Kick_Walk,
    Magic_Walk,
    ReceiveDamage
}
public class AnimationController
{
    public Animator Animator { get; private set; }
    public string currentAnimaton;
    private float lastCallAnimation;
    private float delayBeetwenAnimations;
    public AnimationController(Animator animator)
    {
        Animator = animator;
    }
    public void ChangeAnimationState(PersonState state)
    {
        if (currentAnimaton == state.ToString()) return;

        Animator.Play(state.ToString());
        currentAnimaton = state.ToString();
    }
    /// <summary>
    /// нужно чтобы stickman наследовался от общего класса с подобными файтерами
    /// </summary>
    /// <param name="stickman"></param>
    /// <returns></returns>
    public IEnumerator CorExitToState(Stickman stickman, PersonState endState)
    {
        yield return new WaitForFixedUpdate();
        float lenght =  GetCurrentAnimatorStateLength();

        //GetCurrentAnimationName();
        //Debug.Log(lenght + "длина анимации");
        yield return new WaitForSeconds(lenght);
        
        stickman.SetState(endState);
    }
  



    public float GetCurrentAnimatorStateLength()
    {
        AnimatorClipInfo[] clipInfo = Animator.GetCurrentAnimatorClipInfo(0);
  
        AnimationClip clip = clipInfo[0].clip;
        //Debug.Log("анимационный клип длинной:" + clip.length);
        return clip.length;
    }
    public string GetCurrentAnimationName()
    {
        var currentAnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        var currentAnimatorClipInfo = Animator.GetCurrentAnimatorClipInfo(0)[0];
        var currentClip = currentAnimatorClipInfo.clip;
        Debug.Log("имя анимационнного клипа:" + currentClip.name);
        return currentClip.name;
    }
    public AnimationClip GetAnimationClip(PersonState state)
    {
        AnimatorStateInfo info = Animator.GetCurrentAnimatorStateInfo(0);
        RuntimeAnimatorController controller = Animator.runtimeAnimatorController;

        AnimationClip[] clips = controller.animationClips;
        Debug.Log("имя анимационнного клипа:" + GetCurrentAnimationName());
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == state.ToString())
            {
                return clip;
            }
        }

        return null;
    }

    /// <summary>
    /// принимает имя клипа в качестве параметра и переключает аниматор на этот клип
    /// </summary>
    /// <param name="clipName"></param>
    public void SetAnimationClip(PersonState state)
    {
        Animator.PlayInFixedTime(state.ToString(), 0, 0);
    }
}
