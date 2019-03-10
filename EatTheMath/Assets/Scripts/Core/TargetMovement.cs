using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    GameObject targetCircle;
    CircleCollider2D circleCollider;
    GameController gameController;
    public float movementFactor = 1.5f;
    float radiusOfTarget;

    void Start()
    {
        targetCircle = transform.Find("TargetCircle").gameObject;
        gameController = FindObjectOfType<GameController>();
        if (!targetCircle)
        {
            Debug.LogWarning("TargetCircle child is missing");
        } else
        {
            SetColliderRadiusToRadiusOfTarget();
        }
        if (!gameController)
        {
            Debug.LogWarning("Game Controller is missing");
        }
    }

    void Update()
    {
        if(!gameController || !targetCircle)
        {
            return;
        }

        if (gameController.gameIsActive)
        {
            MoveTarget();
        }
        TargetIsOffScreen();
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

    private void TargetIsOffScreen()
    {
        if(transform.position.x < - gameController.halfHeight * 2)
        {
            Destroy(this.gameObject);
        }
    }

}
