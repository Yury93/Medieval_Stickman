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
        Debug.Log(offsetPositionX);
    }

    public Collider2D GetCollider2D()
    {
        Vector2 myPosition = transform.position  + Vector3.right * offsetPositionX+ Vector3.up * offsetPositionY;
        Collider2D[] colliders = new Collider2D[10];
        int count = Physics2D.OverlapCircleNonAlloc(myPosition, radius, colliders);

        for (int i = 0; i < count; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject != gameObject)
            {
          
                return colliders[i];
            }
        }

        return null;
    }

   

}
