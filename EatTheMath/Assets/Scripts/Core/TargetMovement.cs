using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float movementFactor = 1.5f;
    float radiusOfTarget;
    GameObject targetCircle;
    CircleCollider2D circleCollider;

    void Start()
    {
        targetCircle = transform.Find("TargetCircle").gameObject;
        if (!targetCircle)
        {
            Debug.LogWarning("TargetCircle child is missing");
        } else
        {
            SetColliderRadiusToRadiusOfTarget();
        }
    }

    void Update()
    {
        MoveTarget();
    }

    void SetColliderRadiusToRadiusOfTarget()
    {
        radiusOfTarget = targetCircle.transform.localScale.x / 2;
        circleCollider = GetComponent<CircleCollider2D>();
        if (circleCollider)
        {
            circleCollider.radius = radiusOfTarget;
        }
        else
        {
            Debug.LogWarning("The circle collider component is missing of the target prefab");
        }
    }

    void MoveTarget()
    {
        float movement = Time.deltaTime * movementFactor;
        transform.position = new Vector2(transform.position.x - movement, transform.position.y);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

}
