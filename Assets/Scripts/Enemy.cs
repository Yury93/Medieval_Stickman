using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : FighterEntity
{
    [SerializeField] protected float speed;

    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float radiusAttack, offsetRadiusAttackY;
    [SerializeField] private Collider2D collider2d;
    public float clampDistanceToTarget, distanceStartPursuit;
    public MoveController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }
    public AnimationController AnimatorController { get; private set; }
    private Stickman stickman;

    private void Start()
    {
        stickman = FindAnyObjectByType<Stickman>();
        MoveController = new MoveController(animator, rigidbody);
        AnimatorController = new AnimationController(animator);
        AttackController = new AttackController(this.gameObject, offsetRadiusAttackY, radiusAttack);
        State = PersonState.Idle;
        SetParametrs(100, 10, 10, false);
    }


    public void FixedUpdate()
    {
        MoveToTarget();
        if (stickman.MoveController.IsEnemyColliderIgnore == true && collider2d.enabled == true)
        {
            collider2d.enabled = false;
            rigidbody.gravityScale = 0;
        }
        else if(stickman.MoveController.IsEnemyColliderIgnore == false && collider2d.enabled == false)
        {
            collider2d.enabled = true;
            rigidbody.gravityScale = 1;
        }
    }

    private void MoveToTarget()
    {
        if (stickman == null) return;
        var direction = stickman.transform.position - transform.position;
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

    public void ApplyDamage()
    {
        
        var collider = AttackController.GetCollider2D();
        if (collider != null)
        {
            
            var stickman = collider.GetComponent<Stickman>();
            if (stickman != null )
            {
                if (stickman.MoveController.IsEnemyColliderIgnore == false)
                {
                    stickman.OnDamage(Power);
                }
              
               
            }
        }
        else
        {
            AnimatorController.CorExitToState(this, PersonState.Idle);
        }

        //anima
        //Debug.Log("включать состоние idle чтобы обхект выходил из анимации");
        //AnimatorController.CorExitToState(this, PersonState.Idle);

    }
    public override void OnDamage(int damage)
    {
       base.OnDamage(damage);
        if (CurrentHp > 0)
        {
            if (MoveController.IsGrounded == false) return;
            //State = PersonState.ReceiveDamage;
            //AnimatorController.ChangeAnimationState(PersonState.ReceiveDamage);

            //StartCoroutine(A nimatorController.CorExitToState(this, PersonState.Idle));
        }
    }
    protected override void OnDeath(FighterEntity fighterEntity)
    {
        base.OnDeath(fighterEntity);
        AnimatorController.ChangeAnimationState(PersonState.Death);
        rigidbody.GetComponent<BoxCollider2D>().isTrigger = true;
        rigidbody.isKinematic = true;
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
