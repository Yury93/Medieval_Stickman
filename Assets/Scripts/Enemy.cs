using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State { move, attack }
    [SerializeField] protected float speed;
    [SerializeField] protected State state = State.move;
    [SerializeField] protected float clampDistanceToTarget;
    public float distance;

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

        distance = Vector3.Distance(stickman.transform.position, transform.position);

        if (distance > clampDistanceToTarget)
        {
            state = State.move;

            Vector3 targetDirection = direction.normalized;
            targetDirection.y = 0; // ограничение движения только по оси X

            transform.position += targetDirection * speed * Time.fixedDeltaTime;
        }
    }
}
