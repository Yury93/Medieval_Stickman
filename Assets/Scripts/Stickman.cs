using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stickman : MonoBehaviour
{
    public PersonState State { get; private set; }
    [SerializeField] public Joystick joystick;
    [SerializeField] private Button buttonAttack;
    [SerializeField] private Animator animator;
    [SerializeField] public float speedWalk, jumpSpeed, timeJump;
    [SerializeField] private float radiusAttack, offsetRadiusAttackY;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private LayerMask groundLayer;
    public AttackController AttackController { get; private set; }
    public AnimationController AnimatorController { get; private set; }
    public MoveController MoveController { get; private set; }


    private void Awake()
    {
        AnimatorController = new AnimationController(animator);
        MoveController = new MoveController(animator, rigidbody);
        AttackController = new AttackController(gameObject,offsetRadiusAttackY,radiusAttack);
        buttonAttack.onClick.AddListener(KickEnemy);
    }

    public void KickEnemy()
    {
        if (State == PersonState.Idle )
        {
            
            State = PersonState.Kick;
            AnimatorController.ChangeAnimationState(PersonState.Kick);
            StartCoroutine(AnimatorController.CorExitState(this));
        }
        else if (State == PersonState.Kick)
        {
            StartCoroutine(AnimatorController.CorDoubleCallAnimation(this,KickEnemy, PersonState.Kick));
        }
    }

    private void FixedUpdate()
    {
        if (State != PersonState.Kick)
        {
            MoveController.Move(joystick.Horizontal, speedWalk);

            if (joystick.Vertical > 0.5f && MoveController.IsGrounded)
            {
                StartCoroutine(MoveController.CorJump(jumpSpeed, timeJump, groundLayer));

            }
            State = MoveController.State;
            AnimatorController.ChangeAnimationState(MoveController.State);
            
        }
    }
    public void SetState(PersonState state)
    {
        State = state;
    }



#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        Vector2 myPosition;
        myPosition = new Vector3(transform.position.x, transform.position.y + offsetRadiusAttackY, transform.position.z);
        UnityEditor.Handles.DrawWireDisc(myPosition, transform.forward, radiusAttack);
    }

#endif
}
