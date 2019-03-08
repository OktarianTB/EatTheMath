using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    Player player;
    TextMeshProUGUI textMesh;
    GameController gameController;
    public GameObject scoreText;
    public int scoreToWin = 1000;
    public int currentScore = 0;
    float generalTime;
    float timeInMinutes;
    string timeInSeconds;

    void Start()
    {
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();
        textMesh = scoreText.GetComponent<TextMeshProUGUI>();

        if (!player)
        {
            Debug.LogWarning("Player is missing");
        }
        if (!scoreText)
        {
            Debug.LogWarning("ScoreText is missing");
        }
        if (!textMesh)
        {
            Debug.LogWarning("Text Mesh component is missing off of the score text game object");
        }
        if (!gameController)
        {
            Debug.LogWarning("Game Controller is missing");
        }
    }
    
    void Update()
    {
        if (!player || !scoreText ||!textMesh ||!gameController)
        {
            return;
        }

        if (gameController.gameIsActive)
        {
            UpdateTimeScoreText();
        }
    }

    public void ManagePoints(int points)
    {
        currentScore += points;
        player.circleRadius += points * player.radiusIncrementValue;
        player.UpdateText(currentScore);
    }

    private void UpdateTimeScoreText()
    {
        generalTime += Time.deltaTime;
        if(generalTime > 59.5)
        {
            timeInMinutes++;
            generalTime = 0;
        } 

        timeInSeconds = Mathf.Round(generalTime).ToString();

        if(timeInSeconds.Length == 1)
        {
            timeInSeconds = "0" + timeInSeconds;
        }
        textMesh.text = timeInMinutes.ToString() + ":" + timeInSeconds;
    }

}
