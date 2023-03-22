using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stickman : FighterEntity
{
    public enum PlatformType { PC,MOBILE }
    [SerializeField] private PlatformType platformType = PlatformType.PC;
    [SerializeField] public Joystick joystick;
    [SerializeField] private Button buttonIdleAttack, buttonIdleMagic, buttonWalkAttack, buttonWalkMagic,buttonJerk;
    [SerializeField] private Animator animator;
    [SerializeField] public float speedWalk,speedWalkIsGround,speedWalkIsAir, jumpSpeed, timeJump;
    [SerializeField] private float radiusAttack, offsetRadiusAttackY, offsetRadiusAttackX;
    [SerializeField] private Rigidbody2D rigidbody;
    public AttackController AttackController { get; private set; }
    public AnimationController AnimatorController { get; private set; }
    public MoveController MoveController { get; private set; }


    private void Awake()
    {
        AnimatorController = new AnimationController(animator);
        MoveController = new MoveController(animator, rigidbody);
        AttackController = new AttackController(gameObject,offsetRadiusAttackY,radiusAttack);
        buttonIdleAttack.onClick.AddListener(OnKickIfPersonIdle);
        buttonIdleMagic.onClick.AddListener(OnMagicAttackIfPersonIdle);
        buttonWalkAttack.onClick.AddListener(OnKickIfPersonWalk);
        buttonWalkMagic.onClick.AddListener(OnMagicAttackIfPersonWalk);
        buttonJerk.onClick.AddListener(OnJerk);
        if (Application.isMobilePlatform)
        {
            Debug.Log("Игрок играет на мобильном устройстве");
            platformType = PlatformType.MOBILE;
        }
        else
        {
            Debug.Log("Игрок играет на ПК");
            platformType = PlatformType.PC;
            joystick.gameObject.SetActive(false);
        }
        SetParametrs(100, 10, 10, false);
        rigidbody.inertia = 1;
    }

    public void OnKickIfPersonIdle()
    {
        if (State == PersonState.Idle )
        {
            State = PersonState.Kick_Idle;
            //MoveController.SetState(State);
          AnimatorController.ChangeAnimationState(PersonState.Kick_Idle);
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));
            
        }
    }
    public void OnMagicAttackIfPersonIdle()
    {
        if (State == PersonState.Idle)
        {
            State = PersonState.Magic_Idle;
            //MoveController.SetState(State);
            AnimatorController.ChangeAnimationState(PersonState.Magic_Idle);
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));
        }
    }
    public void OnKickIfPersonWalk()
    {
        if (State == PersonState.Walk)
        {
            State = PersonState.Kick_Walk;
            AnimatorController.ChangeAnimationState(PersonState.Kick_Walk);
            var length = AnimatorController.GetCurrentAnimatorStateLength();
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Walk));
            AutoWalk(length);

        }
    }
    public void OnMagicAttackIfPersonWalk()
    {
        if (State == PersonState.Walk )
        {
            State = PersonState.Magic_Walk;
            AnimatorController.ChangeAnimationState(PersonState.Magic_Walk);
            var length = AnimatorController.GetCurrentAnimatorStateLength();
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Walk));
            AutoWalk(length);

        }
    }
    public void OnJerk()
    {
        if (State == PersonState.Walk )
        {
            var state = State;
            State = PersonState.Jerk;
            bool isRight = animator.transform.rotation.y > 0 ? true : false;
            if (platformType == PlatformType.MOBILE)
            {
                StartCoroutine(MoveController.CorJerk(joystick.Horizontal, 4f, 0.5f));
            }
            else
            {
                
                float direction;
                if (isRight) direction = 1;
                else direction = -1;
                StartCoroutine(MoveController.CorJerk(direction, 3f, 0.5f));
                

            }
            if (isRight) AttackController.SetOffsetAttackXAxis(-offsetRadiusAttackX);
            else AttackController.SetOffsetAttackXAxis(offsetRadiusAttackX);
            State = state;

            Debug.Log(offsetRadiusAttackX);
        }
    }

    private void AutoWalk(float length)
    {
        if (platformType == PlatformType.MOBILE)
        {
            StartCoroutine(MoveController.CorAutoWalk(joystick.Horizontal, speedWalk, length));
        }
        else
        {
            bool isRight = animator.transform.rotation.y > 0 ? true : false;
            float direction;
            if (isRight) direction = 1;
            else direction = -1;
            StartCoroutine(MoveController.CorAutoWalk(direction, speedWalk, length));
        }
    }

  
    public void ApplyDamage(int power)
    {
      


        var collider = AttackController.GetCollider2D();
        if (collider != null)
        {

            //Debug.Log("Нанёс урон: " + collider.gameObject.namse);
           var enemy = collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.OnDamage(power);
            }
        }
        //if(State == PersonState.Kick_Walk)
        //{
        //    StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Walk));
        //}
        //else
        //{
        //    StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));
        //}
    }
    public void ReceiveDamage()
    {
        if (State == PersonState.Death) return;
        if(CurrentHp > 0)
        { 
        if (MoveController.IsGrounded == false) return;
        State = PersonState.ReceiveDamage;
        AnimatorController.ChangeAnimationState(PersonState.ReceiveDamage);

            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));
        }
    }
    public override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        ReceiveDamage();
    }
    protected override void OnDeath(FighterEntity fighterEntity)
    {
        base.OnDeath(fighterEntity);
        AnimatorController.ChangeAnimationState(PersonState.Death);
    }

    private void RefreshStateButtons()
    {
        if (State == PersonState.Idle )
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


        if (MoveController.IsGrounded)
        {
            buttonJerk.gameObject.SetActive(true);
        }
        else if (!MoveController.IsGrounded)
        {
            buttonJerk.gameObject.SetActive(false);
        }
    }

   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnMagicAttackIfPersonWalk();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnKickIfPersonWalk();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnMagicAttackIfPersonIdle();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnKickIfPersonIdle();
        }
    }
    private void FixedUpdate()
    {
        if (State != PersonState.Kick_Idle 
            && State != PersonState.Magic_Idle 
            && State != PersonState.Kick_Walk 
            && State != PersonState.Magic_Walk 
            && State != PersonState.Jerk
            && State != PersonState.ReceiveDamage
            && State != PersonState.Death)
        {
            RefreshSpeedWalk();
            if (platformType == PlatformType.MOBILE)
            {
                MoveController.Move(joystick.Horizontal, speedWalk);
                if (joystick.Vertical > 0.5f && MoveController.IsGrounded)
                {
                    StartCoroutine(MoveController.CorJump(jumpSpeed, joystick.Horizontal));

                }
            }
            else
            {
                var deltaHorizontal = Input.GetAxis("Horizontal");
                var deltaVerticale = Input.GetAxis("Vertical");
                MoveController.Move(deltaHorizontal, speedWalk);
                if (deltaVerticale > 0.5f && MoveController.IsGrounded)
                {
                    StartCoroutine(MoveController.CorJump(jumpSpeed, deltaHorizontal));

                }
            }


            State = MoveController.State;
       
            AnimatorController.ChangeAnimationState(MoveController.State);
        }

        RefreshStateButtons();
    }

    private void RefreshSpeedWalk()
    {
        if (MoveController.IsGrounded)
        {
            speedWalk = speedWalkIsGround;
        }
        else
        {
            speedWalk = speedWalkIsAir;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ground = collision.gameObject.GetComponent<Ground>();
       if(ground != null)
        {
            MoveController.SetGrounded(true);
        }
        else if(MoveController.IsGrounded == false)
        {
            rigidbody.drag = 0;
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        Vector2 myPosition;
        myPosition = new Vector3(transform.position.x + offsetRadiusAttackX, transform.position.y + offsetRadiusAttackY, transform.position.z);
        UnityEditor.Handles.DrawWireDisc(myPosition, transform.forward, radiusAttack);
    }

#endif
}
