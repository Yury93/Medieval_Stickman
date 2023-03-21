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
    public float clampDistanceToTarget, distanceStartPursuit;
    public MoveController MoveController { get; private set; }
    public AttackController AttackController { get; private set; }
    public AnimationController AnimationController { get; private set; }
    private Stickman stickman;

    private void Start()
    {
        stickman = FindAnyObjectByType<Stickman>();
        MoveController = new MoveController(animator, rigidbody);
        AnimationController = new AnimationController(animator);
        AttackController = new AttackController(this.gameObject, offsetRadiusAttackY, radiusAttack);
        State = PersonState.Idle;
        SetParametrs(100, 10, 10, false);
    }


    public void FixedUpdate()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        var direction = stickman.transform.position - transform.position;
        var distanceToTarget = direction.magnitude;
        if (State != PersonState.Kick_Idle  )
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


            AnimationController.ChangeAnimationState(State);
        }
    }

    public void ApplyDamage()
    {
        var collider = AttackController.GetCollider2D();
        if (collider != null)
        {
            Debug.Log("Нанёс урон: " + collider.gameObject.name);
            var stickman = collider.GetComponent<Stickman>();
            if(stickman != null)
            {
                stickman.OnDamage(Power);
            }
        }
        else
        {

            State = PersonState.Idle;
        }
        //anima
        Debug.Log("включать состоние idle чтобы обхект выходил из анимации");
        AnimationController.CorExitToState(this, PersonState.Idle);

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
