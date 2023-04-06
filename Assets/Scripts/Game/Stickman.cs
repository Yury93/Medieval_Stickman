using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stickman : FighterEntity
{
  
    public enum PlatformType { PC,MOBILE }
    [SerializeField] public int id;
    [SerializeField] private PlatformType platformType = PlatformType.PC;
    [SerializeField] public Joystick joystick;
    [SerializeField] private Button buttonIdleAttack, buttonIdleMagic, buttonWalkAttack, buttonWalkMagic,buttonJerk,buttonHelp;
    [SerializeField] private Animator animator;
    [SerializeField] public float speedWalk,speedWalkIsGround,speedWalkIsAir, jumpSpeed, timeJump,helpDelay,jerkDelay;
    [SerializeField] private float radiusAttack, offsetRadiusAttackY, offsetRadiusAttackX;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private TextMeshProUGUI helpDelayTxt, jerkDelayText;
    [SerializeField] private GameObject batsEffect;
    [SerializeField] private Sprite portret;
    public Sprite Portret => portret;
    public StickmanSpellProperty CurrentSpell { get; private set; }
    public AttackController AttackController { get; private set; }
    public AnimationController AnimatorController { get; private set; }
    public MoveController MoveController { get; private set; }
    private Coroutine corStateExit;
    public int MaxMana { get; private set; }
    public int MaxHP { get; private set; }
    public int MaxArmor { get; private set; }
    public event Action OnDeathStickman;

    public UpgradeGameSystem UpgradeGameSystem { get; private set; }
    public void Init()
    {
        UpgradeGameSystem = CoreEnivroment.Instance.upgradeGameSystem;
        AnimatorController = new AnimationController(animator);
        MoveController = new MoveController(animator,this, rigidbody);
        AttackController = new AttackController(gameObject,offsetRadiusAttackY,radiusAttack);
        buttonIdleAttack.onClick.AddListener(OnKickIfPersonIdle);
        buttonIdleMagic.onClick.AddListener(OnMagicAttackIfPersonIdle);
        buttonWalkAttack.onClick.AddListener(OnKickIfPersonWalk);
        buttonWalkMagic.onClick.AddListener(OnMagicAttackIfPersonWalk);
        buttonJerk.onClick.AddListener(OnJerk);
        buttonHelp.onClick.AddListener(OnHelp);

        CurrentHp += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Hitpoints);
        Armor += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Armor);
        Mana += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Mana);
        Power += StickmanSaveUpgrader.GetStickmanParametrs(StickmanSaveUpgrader.SickmanParametr.Power);
        SetMaxParameters(CurrentHp, Armor, Mana);

        CoreEnivroment.Instance.guiStickman.RefreshStartParametrs(CurrentHp, Armor, Mana);
        if (CoreEnivroment.Instance.guiStickman != null)
            CoreEnivroment.Instance.guiStickman.RefreshParametrs((float)CurrentHp, (float)Armor, (float)Mana);
        else
        {
            Debug.LogError("instance == null");
        }

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
        rigidbody.inertia = 1;

        buttonHelp.onClick.AddListener(() => DelayButton(buttonHelp,helpDelay,helpDelayTxt));
        helpDelayTxt.enabled = false;
        buttonJerk.onClick.AddListener(() => DelayButton(buttonJerk, jerkDelay, jerkDelayText));
        jerkDelayText.enabled = false;
    }


    public void SetActiveBatsEffect()
    {
        batsEffect.SetActive(true);

    }
    public void DisactiveBatsEffect()
    {
        batsEffect.SetActive(false);
    }

    private void DelayButton(Button button, float delay,TextMeshProUGUI textDelay)
    {

        StartCoroutine(CorDelay());
        StartCoroutine(ShowDelay());
        
        IEnumerator CorDelay()
        {
            button.interactable = false;
            yield return new WaitForSeconds(delay);
            button.interactable = true;
        }
        IEnumerator ShowDelay()
        {
            textDelay.enabled = true;
         
            var delayTime = delay;
            textDelay.text = delayTime.ToString();
            while (delayTime > 0)
            {
                yield return new WaitForSeconds(1);
                delayTime -= 1;
                textDelay.text = delayTime.ToString();
            }
            textDelay.enabled = false;
        }
    }

    public void SetMaxParameters(int maxHp, int maxArmor, int maxMana)
    {
        MaxHP = maxHp;
        MaxMana = maxMana;
        MaxArmor = maxArmor;
    }

  
    public void SetCurrentSpell(StickmanSpellProperty currentSpell)
    {
        CurrentSpell = currentSpell;
    }

    private void OnHelp()
    {
        if(CoreEnivroment.Instance.tower.towerStickMan.IsEndHelp == true)
            CoreEnivroment.Instance.tower.towerStickMan.Attack();
    }

   

    public void OnKickIfPersonIdle()
    {
        if (State == PersonState.Idle && State != PersonState.Death )
        {
            State = PersonState.Kick_Idle;
          AnimatorController.ChangeAnimationState(PersonState.Kick_Idle);
            if (corStateExit != null) { StopCoroutine(corStateExit); corStateExit = null;  }
            corStateExit = StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));

        }
    }
    public void OnMagicAttackIfPersonIdle()
    {
        if (State == PersonState.Idle && State != PersonState.Death)
        {
            State = PersonState.Magic_Idle;
            AnimatorController.ChangeAnimationState(PersonState.Magic_Idle);
            StartCoroutine(AnimatorController.CorExitToState(this, PersonState.Idle));
        }
    }

    public void OnKickIfPersonWalk()
    {
        if (State == PersonState.Walk && State != PersonState.Death)
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
        if (State == PersonState.Walk && State != PersonState.Death)
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
        if (State == PersonState.Walk && State != PersonState.Death)
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
                StartCoroutine(MoveController.CorJerk(direction, speedWalk * 3, 0.5f));
                

            }
            if (isRight) AttackController.SetOffsetAttackXAxis(-offsetRadiusAttackX);
            else AttackController.SetOffsetAttackXAxis(offsetRadiusAttackX);
            State = state;

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


        if (State == PersonState.Death) return;


            var collider = AttackController.GetCollider2D(this);
        if (collider != null)
        {

           var enemy = collider.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.OnDamage(power);
            }
        }


    }
    public void ReceiveDamage()
    {
        if (State == PersonState.Death) return;
        if(CurrentHp > 0)
        { 
        if (MoveController.IsGrounded == false) return;
        }
    }
    public override void OnDamage(int damage)
    {
        if (State == PersonState.Death) return;
        base.OnDamage(damage);
        ReceiveDamage();
        CoreEnivroment.Instance.guiStickman.RefreshParametrs(CurrentHp, Armor, Mana);
    }
    protected override void OnDeath(FighterEntity fighterEntity)
    {
        if (State == PersonState.Death) return;
        OnDeathStickman?.Invoke();
        SetState(PersonState.Death);
        base.OnDeath(fighterEntity);
        AnimatorController.ChangeAnimationState(PersonState.Death);
        CoreEnivroment.Instance.guiStickman.RefreshParametrs(CurrentHp, Armor, Mana);
        buttonIdleAttack.gameObject.SetActive(false);
        buttonIdleMagic.gameObject.SetActive(false);
        buttonWalkAttack.gameObject.SetActive(false);
        buttonWalkMagic.gameObject.SetActive(false);
        buttonHelp.gameObject.SetActive(false);
        buttonJerk.gameObject.SetActive(false);
        joystick.gameObject.SetActive(false);
    }

    private void RefreshStateButtons()
    {
        if (State == PersonState.Death) return;
        if (State == PersonState.Idle )
        {
            if (buttonIdleAttack.gameObject.activeSelf == false)
            {
                buttonIdleAttack.gameObject.SetActive(true);
                if(UpgradeGameSystem.isMagicIdle)
                buttonIdleMagic.gameObject.SetActive(true);

                buttonWalkAttack.gameObject.SetActive(false);
                buttonWalkMagic.gameObject.SetActive(false);
                buttonJerk.gameObject.SetActive(false);
            }

        }
        else if (State == PersonState.Walk)
        {
            if (buttonWalkAttack.gameObject.activeSelf == false)
            {
                buttonIdleAttack.gameObject.SetActive(false);
                buttonIdleMagic.gameObject.SetActive(false);
                if (UpgradeGameSystem.isKickWalk)
                    buttonWalkAttack.gameObject.SetActive(true);
                if (UpgradeGameSystem.isMagicWalk)
                    buttonWalkMagic.gameObject.SetActive(true);
                if (UpgradeGameSystem.isJerk)
                    buttonJerk.gameObject.SetActive(true);
            }
        }
        if(UpgradeGameSystem.isHelp)
        {
            buttonHelp.gameObject.SetActive(true);
        }

        if (MoveController.IsGrounded)
        {
            if (UpgradeGameSystem.isJerk)
                buttonJerk.gameObject.SetActive(true);
        }
        else if (!MoveController.IsGrounded)
        {
            buttonJerk.gameObject.SetActive(false);
        }
    }

   

   
    private void FixedUpdate()
    {
        RefreshStateButtons();
        if (State == PersonState.Death) return;


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
            AnimatorController.ChangeAnimationState(State);


        }
        if(AnimatorController.GetCurrentAnimationName() == "Idle")
        {
            SetState(PersonState.Idle);
        }
       
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
