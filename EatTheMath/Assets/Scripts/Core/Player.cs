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
    GameController gameController;
    ScoreManager scoreManager;
    float minRadius = 0.5f;
    float maxRadius = 2f;
    float maxScore = 1000f;
    float circleIncrease; // value that is added each time update is called to the circle's radius
    public float circleRadius;
    public float radiusIncrementValue; // value per point that is added to the radius

    void Start()
    {
        playerCircle = transform.Find("PlayerCircle").gameObject;
        playerText = transform.Find("PlayerText").gameObject;
        gameController = FindObjectOfType<GameController>();
        textMesh = playerText.GetComponent<TextMeshPro>();
        scoreManager = FindObjectOfType<ScoreManager>();

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
        if (!gameController)
        {
            Debug.LogWarning("GameController is missing");
        }
        if (!scoreManager)
        {
            Debug.LogWarning("Score Manager is missing");
        }
    }

    void Update()
    {
        if (!playerCircle || !playerText || !gameController || !scoreManager)
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

    public void UpdateText(int playerScore) //Score that is in the circle (can be pos & neg)
    {
        gameController.CheckState(playerScore);
        textMesh.text = Mathf.Min(playerScore, 1000).ToString();
    }

    public float GetRadius()
    {
        return playerCircle.transform.localScale.x / 2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionText = collision.transform.Find("TargetValueText").gameObject;
        if (collisionText)
        {
            string text = collisionText.GetComponent<TextMeshPro>().text;
            int points = int.Parse(text);
            print(points);
            ManagePoints(points);
        }
        Destroy(collision.gameObject);
    }

    private void ManagePoints(int points)
    {
       if(points > 0)
        {
            scoreManager.AddToScore(points);
        }
        else if (points < 0)
        {
            scoreManager.RemoveFromScore(points);
        }
       else
        {
            Debug.LogWarning("Point is equal to 0");
        }
    }
}
