using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Player : MonoBehaviour
{
    GameObject playerCircle;
    GameObject playerText;
    TextMeshPro textMesh;
    float minRadius = 0.5f;
    float maxRadius = 2f;
    float maxScore = 1000f;
    public float radiusIncrementValue; // value that is added to the radius wanted as final
    float circleIncrease; // value that is added each time update is called to the circle's radius
    [Range(0.5f,2f)] public float circleRadius;

    void Start()
    {
        playerCircle = transform.Find("PlayerCircle").gameObject;
        playerText = transform.Find("PlayerText").gameObject;
        textMesh = playerText.GetComponent<TextMeshPro>();

        circleRadius = GetRadius();
        radiusIncrementValue = (maxRadius - minRadius) / maxScore;
        circleIncrease = 3 * radiusIncrementValue;

        if (!playerCircle)
        {
            Debug.LogWarning("The child game object 'PlayerCircle' hasn't been found");
        }
        if (!playerText)
        {
            Debug.LogWarning("The child game object 'PlayerText' hasn't been found");
        }
        UpdateText(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerCircle || !playerText)
        {
            return;
        }
        ManageCircleSize();
    }

    private void ManageCircleSize()
    {
        float currentRadius = GetRadius();
        float clampedCircleRadius = Mathf.Clamp(circleRadius, minRadius, maxRadius);
        Vector2 currentScale = new Vector2(playerCircle.transform.localScale.x, playerCircle.transform.localScale.y);
        Vector2 scaleIncrement = new Vector2(circleIncrease, circleIncrease);

        if(Mathf.Abs(currentRadius - clampedCircleRadius) < 0.008) // prevents small changes in the radius
        {
            return;
        }

        if (currentRadius < clampedCircleRadius)
        {
            playerCircle.transform.localScale = currentScale + scaleIncrement;
        }
        else if (currentRadius > clampedCircleRadius)
        {
            playerCircle.transform.localScale = currentScale - scaleIncrement;
        }
    }

    public void UpdateText(int playerScore)
    {
        textMesh.text = playerScore.ToString();
    }

    public float GetRadius()
    {
        return playerCircle.transform.localScale.x / 2;
    }

}
