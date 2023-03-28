using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerEvent : MonoBehaviour
{
    [SerializeField] private Stickman stickman;
    [SerializeField] private Transform cratedMagic;
    [SerializeField] private List<GameObject> magic;
    [SerializeField] private GameObject currentMagic;
    public void ApplyDamageKickIdle()
    {
        stickman.ApplyDamage(stickman.Power);
    }
    public void ApplyDamageKickWalk()
    {
        stickman.ApplyDamage(stickman.Power/3);
    }
    public void CreateMagic()
    {
        Instantiate(currentMagic, cratedMagic.position, Quaternion.identity);
    }
}
