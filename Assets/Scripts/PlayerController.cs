using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController2D character;

    void Start()
    {
        // Получаем скрипт CharacterController2D из компонента на объекте с костями персонажа
        character = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        // Получение ввода от игрока
        float horizontalInput = Input.GetAxis("Horizontal");
        bool jumpInput = Input.GetButtonDown("Jump");
        Debug.Log(jumpInput + " crouch");
        bool crouchInput = Input.GetButtonDown("Crouch");
        bool attack1Input = Input.GetButtonDown("Attack1");
        bool attack2Input = Input.GetButtonDown("Attack2");

        // Вызов соответствующих функций CharacterController2D в зависимости от ввода игрока
        character.Move(horizontalInput, crouchInput, jumpInput);

        if (attack1Input)
        {
            character.Attack1();
        }

        if (attack2Input)
        {
            character.Attack2();
        }
    }
}
