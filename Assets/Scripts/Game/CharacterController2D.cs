using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController2D : MonoBehaviour
{
    // ��������� ����������� � ������ ���������
    [SerializeField] private float m_JumpForce = 400f;                          // ���� ������
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // �������� ������ ��� ����������
    [SerializeField] private float m_MovementSmoothing = .05f;                  // ��������� ����������� ���������
    [SerializeField] private bool m_AirControl = false;                          // ����� �� �������� �������� ����������� �������� � �������?
    [SerializeField] private LayerMask m_WhatIsGround;                          // ����, ������������, ��� �������� ������, ����� �������� ��� �������

    const float k_GroundedRadius = .2f; // ������ ��� �����������, ������������� �� �������� � ������.
    private bool m_Grounded;            // �������� �� �����?
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // �������� ������� ������?
    private Vector3 m_Velocity = Vector3.zero;

    // ��������� ����� ���������
    [SerializeField] GameObject attackBox;    // ������, �� ������� ������������� ��������� �����
    [SerializeField] float attackRange = 0.5f; // ��������� �����
    [SerializeField] float attackForce = 5f;  // ���� �����

    // ������������ ��������� ��������� ��������
    enum CharacterState
    {
        idle,
        run,
        jump,
        fall,
        attack1,
        attack2
    }

    CharacterState currentState = CharacterState.idle;  // ������� ��������� ��������

    [SerializeField]private Animator animator;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // ����������, ��������� �� �������� �� �����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }

        // ������������� ������ �������� � ����������� �� ��������� ���������
        if (!m_Grounded)
        {
            if (m_Rigidbody2D.velocity.y > 0)
            {
                currentState = CharacterState.jump;
            }
            else
            {
                currentState = CharacterState.fall;
            }
        }
        else
        {
            if (m_Rigidbody2D.velocity.x != 0)
            {
                currentState = CharacterState.run;
            }
            else
            {
                currentState = CharacterState.idle;
            }
        }

        //animator.SetInteger("state", (int)currentState);
    }

    public void Move(float move, bool crouch, bool jump)
    {
        // ���� �������� ��������� �� �����
        if (m_Grounded || m_AirControl)
        {
            // ���� �������� ������
            if (crouch)
            {
                move *= m_CrouchSpeed;
            }

            // ������������� �������� �����������
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // � ��������� ��������� � ������ �����������
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // ���� �������� ��������� ������, �� ������� �����...
            if (move > 0 && !m_FacingRight)
            {
                // ...������������� ���������
                Flip();
            }
            // � ��������
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }
        // ���� �������� ��������� � ������ � �� ����� ������ ����������� ��������
        else if (m_AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        // ���� �������� �� ����� � ����� ��������
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    // ������� ��� ��������, ��������� �� �������� ������
    private void Flip()
    {
        // ������ �����������, � ������� ������� �������� (����� �� ��� X �������� �� -1)
        m_FacingRight = !m_FacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // ������� ��� ���������� ������
    public void Attack1()
    {
        currentState = CharacterState.attack1;
        animator.SetTrigger("Kick");
    }

    public void Attack2()
    {
        currentState = CharacterState.attack2;
        animator.SetTrigger("Kick");
    }

    // ������� ��� ������ �� �������� �����
    public void DoAttack()
    {
        // ������� ��������� �����
        Collider2D attack = Instantiate(attackBox, transform.position + new Vector3(attackRange * (m_FacingRight ? 1 : -1), 0, 0), Quaternion.identity).GetComponentInChildren<Collider2D>();
        // ���������� ��� � ������ �������
        attack.transform.localScale = new Vector3((m_FacingRight ? 1 : -1), 1, 1);
        // ������ ���� �����
        //attack.GetComponent<AttackController>().SetForce(attackForce);
        // ��������� ������������� ���������� ����� ����������, ������� ��� ������
        Physics2D.IgnoreCollision(attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

}


