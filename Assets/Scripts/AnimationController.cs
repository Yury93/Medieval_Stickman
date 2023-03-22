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
    ReceiveDamage,
    Death,
    Jerk
}
public class AnimationController
{
    public Animator Animator { get; private set; }
    public string currentAnimaton;
    private float lastCallAnimation;
    private float delayBeetwenAnimations;

    private AnimatorClipInfo[] lastClipInfo;
    private AnimationClip lastClip;

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

    public IEnumerator CorExitToState(FighterEntity fighterEntity, PersonState endState)
    {
            yield return new WaitForFixedUpdate();
            float lenght = GetCurrentAnimatorStateLength();
            yield return new WaitForSeconds(lenght);
           
            fighterEntity.SetState(endState);

     
    }




    float lastCallTime;
    public float GetCurrentAnimatorStateLength()
    {
        AnimatorClipInfo[] clipInfo = Animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length == 0)
        {
            //Debug.LogError("Длина клипа почему-то равна нулю");
            return lastClip.length;
        }
        AnimationClip currentClip = clipInfo[0].clip;

        float currentTime = Time.realtimeSinceStartup;
        float timeSinceLastCall = currentTime - lastCallTime;
        if (timeSinceLastCall < 0.1f)
        {
            // Если метод вызывается слишком часто, вернуть значение из предыдущего вызова
            return lastClip.length;
        }

        lastCallTime = currentTime;
        lastClip = currentClip;
        return currentClip.length;
    }


    public string GetCurrentAnimationName()
    {
        var currentAnimatorStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        var currentAnimatorClipInfo = Animator.GetCurrentAnimatorClipInfo(0)[0];
        var currentClip = currentAnimatorClipInfo.clip;
       
        return currentClip.name;
    }
    public AnimationClip GetAnimationClip(PersonState state)
    {
        AnimatorStateInfo info = Animator.GetCurrentAnimatorStateInfo(0);
        RuntimeAnimatorController controller = Animator.runtimeAnimatorController;

        AnimationClip[] clips = controller.animationClips;

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
