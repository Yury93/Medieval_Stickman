using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterController2D : MonoBehaviour
{
    // Настройки перемещения и прыжка персонажа
    [SerializeField] private float m_JumpForce = 400f;                          // сила прыжка
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // скорость ходьбы при приседании
    [SerializeField] private float m_MovementSmoothing = .05f;                  // затухание перемещения персонажа
    [SerializeField] private bool m_AirControl = false;                          // может ли персонаж изменять направление движения в воздухе?
    [SerializeField] private LayerMask m_WhatIsGround;                          // слой, определяющий, что является землей, чтобы персонаж мог прыгать

    const float k_GroundedRadius = .2f; // Радиус для определения, соприкасается ли персонаж с землей.
    private bool m_Grounded;            // Персонаж на земле?
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // Персонаж смотрит вправо?
    private Vector3 m_Velocity = Vector3.zero;

    // Настройки атаки персонажа
    [SerializeField] GameObject attackBox;    // объект, на котором располагается коллайдер атаки
    [SerializeField] float attackRange = 0.5f; // дальность атаки
    [SerializeField] float attackForce = 5f;  // сила атаки

    // Перечисление доступных состояний анимации
    enum CharacterState
    {
        idle,
        run,
        jump,
        fall,
        attack1,
        attack2
    }

    CharacterState currentState = CharacterState.idle;  // текущее состояние анимации

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

        // Определяем, находится ли персонаж на земле
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
            }
        }

        // Устанавливаем статус анимации в зависимости от состояния персонажа
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
        // Если персонаж двигается по земле
        if (m_Grounded || m_AirControl)
        {
            // Если персонаж присел
            if (crouch)
            {
                move *= m_CrouchSpeed;
            }

            // Устанавливаем скорость перемещения
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // И применяем затухание в данном направлении
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // Если персонаж двигается вправо, но смотрит влево...
            if (move > 0 && !m_FacingRight)
            {
                // ...разворачиваем персонажа
                Flip();
            }
            // И наоборот
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }
        // Если персонаж находится в прыжке и не может менять направление движения
        else if (m_AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }

        // Если персонаж на земле и может прыгнуть
        if (m_Grounded && jump)
        {
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    // Функция для проверки, направлен ли персонаж вправо
    private void Flip()
    {
        // Меняем направление, в который смотрит персонаж (скейл по оси X умножаем на -1)
        m_FacingRight = !m_FacingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    // Функции для управления атакой
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

    // Функция для вызова из анимации атаки
    public void DoAttack()
    {
        // Создаем коллайдер атаки
        Collider2D attack = Instantiate(attackBox, transform.position + new Vector3(attackRange * (m_FacingRight ? 1 : -1), 0, 0), Quaternion.identity).GetComponentInChildren<Collider2D>();
        // Направляем его в нужную сторону
        attack.transform.localScale = new Vector3((m_FacingRight ? 1 : -1), 1, 1);
        // Задаем силу атаки
        //attack.GetComponent<AttackController>().SetForce(attackForce);
        // Добавляем игнорирование коллайдера атаки персонажем, который его создал
        Physics2D.IgnoreCollision(attack.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

}


