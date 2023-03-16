using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State { move,attack}
    [SerializeField] protected float speed;
    [SerializeField] protected State state = State.move;
    [SerializeField] protected float clampDistanceToTarget;

    private Stickman stickman;
    private void Start()
    {
        stickman = FindAnyObjectByType<Stickman>();
    }
     void Update()
    {
        MoveToTarget();
    }

    public virtual void MoveToTarget()
    {
        Vector3 direction = (stickman.transform.position - transform.position);
        float distance = direction.magnitude;
        if (distance > clampDistanceToTarget)
        {
            state = State.move;
            transform.Translate(direction.normalized.x * speed * Time.deltaTime, 0, 0);
        }
        else
        {

        }
    }
}
