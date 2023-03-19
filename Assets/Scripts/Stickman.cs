using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stickman : MonoBehaviour
{
    public PersonState State { get; private set; }
    public enum PlatformType { PC,MOBILE }
    [SerializeField] private PlatformType platformType = PlatformType.PC;
    [SerializeField] public Joystick joystick;
    [SerializeField] private Button buttonIdleAttack, buttonIdleMagic, buttonWalkAttack, buttonWalkMagic;
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
        buttonIdleAttack.onClick.AddListener(KickIfPersonIdle);
        buttonIdleMagic.onClick.AddListener(MagicAttackIfPersonIdle);
        buttonWalkAttack.onClick.AddListener(KickIfPersonWalk);
        buttonWalkMagic.onClick.AddListener(MagicAttackIfPersonWalk);
        if (Application.isMobilePlatform)
        {
            Debug.Log("Игрок играет на мобильном устройстве");
            platformType = PlatformType.MOBILE;
        }
        else
        {
            Debug.Log("Игрок играет на ПК");
            platformType = PlatformType.PC;
        }
    }

    public void KickIfPersonIdle()
    {
        if (State == PersonState.Idle )
        {
            State = PersonState.Kick_Idle;
            //MoveController.SetState(State);
          AnimatorController.ChangeAnimationState(PersonState.Kick_Idle);
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));
        }
    }
    public void MagicAttackIfPersonIdle()
    {
        if (State == PersonState.Idle)
        {
            State = PersonState.Magic_Idle;
            //MoveController.SetState(State);
            AnimatorController.ChangeAnimationState(PersonState.Magic_Idle);
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));
        }
    }
    public void KickIfPersonWalk()
    {
        if (State == PersonState.Walk)
        {
            State = PersonState.Kick_Walk;
            AnimatorController.ChangeAnimationState(PersonState.Kick_Walk);
            var length = AnimatorController.GetCurrentAnimatorStateLength();
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Walk));

            StartCoroutine(MoveController.CorAutoWalk(joystick.Horizontal, speedWalk, length));
        }
    }
    public void MagicAttackIfPersonWalk()
    {
        if (State == PersonState.Walk)
        {
            State = PersonState.Magic_Walk;
            AnimatorController.ChangeAnimationState(PersonState.Magic_Walk);
            var length = AnimatorController.GetCurrentAnimatorStateLength();
            StartCoroutine(AnimatorController.CorExitToState(this,PersonState.Walk));
            StartCoroutine(MoveController.CorAutoWalk(joystick.Horizontal,speedWalk,length));
        }
    }
    public void ApplyDamage()
    {
        var collider = AttackController.GetCollider2D();
        if (collider != null)
        {

            Debug.Log("Нанёс урон: " + collider.gameObject.name);
        }
    }
    public void ReceiveDamage()
    {
        if (MoveController.IsGrounded == false) return;
        State = PersonState.ReceiveDamage;
        AnimatorController.ChangeAnimationState(PersonState.ReceiveDamage);
        
        StartCoroutine(AnimatorController.CorExitToState(this,PersonState.Idle));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            MagicAttackIfPersonWalk();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            KickIfPersonWalk();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            MagicAttackIfPersonIdle();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            KickIfPersonIdle();
        }
    }

    private void FixedUpdate()
    {
        if (State != PersonState.Kick_Idle && State != PersonState.Magic_Idle && State != PersonState.Kick_Walk && State != PersonState.Magic_Walk)
        {
            if (platformType == PlatformType.MOBILE)
            {
                MoveController.Move(joystick.Horizontal, speedWalk);
                if (joystick.Vertical > 0.5f && MoveController.IsGrounded)
                {
                    StartCoroutine(MoveController.CorJump(jumpSpeed, timeJump, groundLayer));

                }
            }
            else
            {
                var deltaHorizontal = Input.GetAxis("Horizontal");
            
                MoveController.Move(deltaHorizontal, speedWalk);

                var deltaVerticale = Input.GetAxis("Vertical");
                if (deltaVerticale > 0.5f && MoveController.IsGrounded)
                {
                    StartCoroutine(MoveController.CorJump(jumpSpeed, timeJump, groundLayer));

                }
            }

           
            State = MoveController.State;
            AnimatorController.ChangeAnimationState(MoveController.State);
        }
       
        RefreshStateButtons();
    }

    private void RefreshStateButtons()
    {
        if (State == PersonState.Idle)
        {
            if (buttonIdleAttack.gameObject.activeSelf == false)
            {
                buttonIdleAttack.gameObject.SetActive(true);
                buttonIdleMagic.gameObject.SetActive(true);
                buttonWalkAttack.gameObject.SetActive(false);
                buttonWalkMagic.gameObject.SetActive(false);
            }

        }
        else if (State == PersonState.Walk)
        {
            if (buttonWalkAttack.gameObject.activeSelf == false)
            {
                buttonIdleAttack.gameObject.SetActive(false);
                buttonIdleMagic.gameObject.SetActive(false);
                buttonWalkAttack.gameObject.SetActive(true);
                buttonWalkMagic.gameObject.SetActive(true);
            }
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
