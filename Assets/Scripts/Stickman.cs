using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stickman : MonoBehaviour
{
    [SerializeField] public Joystick joystick;
    [SerializeField] private Button buttonAttack;
    [SerializeField] public Transform animatorTransform;
    [SerializeField] private Animator animator;
    [SerializeField] public float speedWalk, jumpSpeed, timeJump,heightJump;
    [SerializeField] private float radiusAttack, offsetRadiusAttackY;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private LayerMask groundLayer;
    public FighterAttack fighterAttack { get; private set; }
    public AnimatorStickman AnimatorMan { get; private set; }
    public MoveStickMan MoveMan { get; private set; }
    private void Awake()
    {
        AnimatorMan = new AnimatorStickman(animator);
        MoveMan = new MoveStickMan(animator,AnimatorMan, rigidbody);
        fighterAttack = new FighterAttack(gameObject,offsetRadiusAttackY,radiusAttack);
        buttonAttack.onClick.AddListener(KickEnemy);
    }

    private void KickEnemy()
    {
      var collider =  fighterAttack.GetCollider2D();
        if(collider != null && MoveMan.IsGrounded )
        {
            if (MoveMan.State == MoveStickMan.StateMoving.idle)
            {
                AnimatorMan.SetKick(true);
                StartCoroutine(CorKickFinish());
                IEnumerator CorKickFinish()
                {
                    yield return new WaitForSeconds(1.3f);
                    AnimatorMan.SetKick(false);
                }
            }
            else
            {
                AnimatorMan.SetKick(false);
            }
        }
        else
        {
            AnimatorMan.SetKick(false);
        }
    }

    private void FixedUpdate()
    {
        MoveMan.Move(joystick.Horizontal,speedWalk);
        if (joystick.Vertical > 0.5f && MoveMan.IsGrounded)
        {
            StartCoroutine(MoveMan.CorJump(jumpSpeed, timeJump, groundLayer));
        
        }
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
