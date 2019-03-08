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
    CircleCollider2D circleCollider;
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
        scoreManager = FindObjectOfType<ScoreManager>();
        circleCollider = GetComponent<CircleCollider2D>();

        circleRadius = GetRadius();
        radiusIncrementValue = (maxRadius - minRadius) / maxScore;
        circleIncrease = 3 * radiusIncrementValue;

        if (!playerCircle)
        {
            Debug.LogWarning("The child game object 'PlayerCircle' hasn't been found");
        }
        if (playerText)
        {
            textMesh = playerText.GetComponent<TextMeshPro>();
        }
        else
        {
            Debug.LogWarning("The child game object 'PlayerText' hasn't been found");
        }
        if (!gameController)
        {
            Debug.LogWarning("GameController is missing");
        }
        if (!textMesh)
        {
            Debug.LogWarning("Text mesh component is missing off the player text");
        }
        if (!scoreManager)
        {
            Debug.LogWarning("Score Manager is missing");
        }
        if (!circleCollider)
        {
            Debug.LogWarning("Circle Collider component is missing off of player");
        }
    }

    void Update()
    {
        if (!playerCircle || !playerText || !gameController || !scoreManager ||!textMesh ||!circleCollider)
        {
            return;
        }
        ManageCircleSize();
    }

    private void ManageCircleSize()
    {
        float currentRadius = GetRadius();
        float clampedCircleRadius = Mathf.Clamp(circleRadius, minRadius, maxRadius);
        circleCollider.radius = clampedCircleRadius;

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
            scoreManager.ManagePoints(points);
        }
        Destroy(collision.gameObject);
    }
}
