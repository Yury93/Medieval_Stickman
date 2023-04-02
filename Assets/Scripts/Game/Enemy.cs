using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : FighterEntity
{
    [SerializeField] protected float speed;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float radiusAttack, offsetRadiusAttackY;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private int exp;
    [SerializeField] private TextMeshProUGUI levelEnemy;
    public int Exp => exp;
    public float clampDistanceToTarget, distanceStartPursuit;
    public Rigidbody2D Rigidbody => rigidbody;
    public MoveController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }
    public AnimationController AnimatorController { get; private set; }
    public Stickman stickman;
    public  Tower tower;
    public bool Initialized { get; private set; }
    public Action<Enemy> OnEnemyDeath;

   public void Init()
    {
        stickman = FindAnyObjectByType<Stickman>();
        tower = FindAnyObjectByType<Tower>();
        MoveController = new MoveController(animator,this, rigidbody);
        AnimatorController = new AnimationController(animator);
        AttackController = new AttackController(this.gameObject, offsetRadiusAttackY, radiusAttack);
        State = PersonState.Idle;
        Initialized = true;
        collider2d.enabled = true;
        levelEnemy.text = "Уровень: " + exp;
    }
    public void SetTarget(Stickman stickman, Tower tower)
    {
        this.stickman = stickman;
        this.tower = tower;
    }
   
    public void FixedUpdate()
    {
        if (State == PersonState.Death) return;
        if (Initialized == false) return;

        if (stickman)
        {
            MoveToTarget(stickman.transform);
        }
        else if( tower)
        {
            MoveToTarget(tower.transform);
        }
        if (stickman && stickman.MoveController.IsEnemyColliderIgnore == true && collider2d.enabled == true)
        {
            collider2d.enabled = false;
            rigidbody.gravityScale = 0;
        }
        else if(stickman && stickman.MoveController.IsEnemyColliderIgnore == false && collider2d.enabled == false)
        {
            collider2d.enabled = true;
            rigidbody.gravityScale = 1;
        }
        if (AnimatorController.GetCurrentAnimationName() == "Idle" || tower == null && stickman == null)
        {
            SetState(PersonState.Idle);
            AnimatorController.ChangeAnimationState(PersonState.Idle);
        }
 
    }

    private void MoveToTarget(Transform target)
    {
        if (target == null) return;
        var direction = target.position - transform.position;
        var distanceToTarget = direction.magnitude;
        if ( State != PersonState.Death && State != PersonState.ReceiveDamage)
        {
            if (distanceToTarget < distanceStartPursuit)
            {
                if (clampDistanceToTarget <= distanceToTarget)
                {
                    MoveController.Move(direction.normalized.x, speed);
                    State = PersonState.Walk;
                }
                else
                {
                    MoveController.RotateToDirectionPerson(direction.normalized.x);
                    State = PersonState.Kick_Idle;
                }
            }
            else
            {
                State = PersonState.Idle;
            }

            AnimatorController.ChangeAnimationState(State);
        }
    }

    public void ApplyAttack()
    {
        if (State == PersonState.Death) return;

        var collider = AttackController.GetCollider2D(this);
        if (collider != null )
        {
            
            var stickman = collider.GetComponent<Stickman>();
            var tower = collider.GetComponent<Tower>();
            if (stickman != null )
            {
                if (stickman.MoveController.IsEnemyColliderIgnore == false)
                {
                    stickman.OnDamage(Power);
                }
            }
            if(tower!= null)
            {
                tower.OnDamage(Power);
            }
        }
        else if(State != PersonState.Idle)
        {
            AnimatorController.CorExitToState(this, PersonState.Idle);
            Debug.Log("Выход из анимации");
        }
    }
    public override void OnDamage(int damage)
    {
        if (State == PersonState.Death) return;
        base.OnDamage(damage);
        if (CurrentHp > 0)
        {
            if (MoveController.IsGrounded == false) return;
        }
    }
    protected override void OnDeath(FighterEntity fighterEntity)
    {
        if (State == PersonState.Death) return;
        base.OnDeath(fighterEntity);
        AnimatorController.ChangeAnimationState(PersonState.Death);
        rigidbody.GetComponent<BoxCollider2D>().enabled = false;
        rigidbody.isKinematic = true;
       
        OnEnemyDeath?.Invoke(this);
        rigidbody.drag = 20;
        rigidbody.mass = 1000;
        rigidbody.velocity = new Vector2(0, 0);
       
    }





#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        Vector2 myPosition;
        myPosition = new Vector3(transform.position.x , transform.position.y + offsetRadiusAttackY, transform.position.z);
        UnityEditor.Handles.DrawWireDisc(myPosition, transform.forward, radiusAttack);
    }

#endif
}
