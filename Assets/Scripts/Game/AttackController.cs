using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController 
{
    private GameObject gameObject;
    private Transform transform;
    private float radius;
    private float offsetPositionY, offsetPositionX ;

    public AttackController(GameObject me,float offsetPosition,float radiusAttack)
    {
        gameObject = me;
        transform = gameObject.transform;
        this.radius = radiusAttack;
        offsetPositionY = offsetPosition;
    }
    public void SetOffsetAttackXAxis(float offsetX)
    {
        offsetPositionX = offsetX;
    }

    public Collider2D GetCollider2D(FighterEntity thisFighter)
    {
        Vector2 myPosition = transform.position  + Vector3.right * offsetPositionX+ Vector3.up * offsetPositionY;
        Collider2D[] colliders = new Collider2D[10];
        int count = Physics2D.OverlapCircleNonAlloc(myPosition, radius, colliders);

        for (int i = 0; i < count; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject != gameObject)
            {
                if (thisFighter as Enemy)
                {
                    var target = colliders[i].GetComponent<Stickman>();
                    var target2 = colliders[i].GetComponent<Tower>();

                    if (target || target2)
                        return colliders[i];
                }
                else if( thisFighter as Stickman)
                {
                    var target = colliders[i].GetComponent<Enemy>();
                   

                    if (target)
                        return colliders[i];
                }
            }
        }

        return null;
    }

   

}
