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

    void SetColliderRadiusToRadiusOfTarget()
    {
        radiusOfTarget = targetCircle.transform.localScale.x / 2;
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = radiusOfTarget;
    }

    void Update()
    {
        MoveTarget();
    }

    void MoveTarget()
    {
        float movement = Time.deltaTime * movementFactor;
        transform.position = new Vector2(transform.position.x - movement, 0f);
    }
}
