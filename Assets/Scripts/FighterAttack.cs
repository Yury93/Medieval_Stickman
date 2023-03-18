using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterAttack 
{
    private GameObject gameObject;
    private Transform transform;
    private float radius;
    private float offsetPositionY;

    public FighterAttack(GameObject me,float offsetPosition,float radiusAttack)
    {
        gameObject = me;
        transform = gameObject.transform;
        this.radius = radiusAttack;
        offsetPositionY = offsetPosition;
    }

    public Collider2D GetCollider2D()
    {
        Vector2 myPosition = transform.position + Vector3.up * offsetPositionY;
        Collider2D[] colliders = new Collider2D[10];
        int count = Physics2D.OverlapCircleNonAlloc(myPosition, radius, colliders);

        for (int i = 0; i < count; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject != gameObject)
            {
                Debug.Log(colliders[i].gameObject.name);
                return colliders[i];
            }
        }

        return null;
    }
    public void Attack()
    {

    }

}
