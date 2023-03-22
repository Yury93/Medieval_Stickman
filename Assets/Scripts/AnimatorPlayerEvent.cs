using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerEvent : MonoBehaviour
{
    [SerializeField] private Stickman stickman
        ;

    public void ApplyDamageKickIdle()
    {
        stickman.ApplyDamage(stickman.Power);
    }
    public void ApplyDamageKickWalk()
    {
        stickman.ApplyDamage(stickman.Power/3);
    }
}
