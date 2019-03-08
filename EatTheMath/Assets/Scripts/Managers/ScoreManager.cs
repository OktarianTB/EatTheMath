using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    Player player;
    GameController gameController;
    TextMeshProUGUI textMesh;
    public GameObject scoreText;
    public int scoreToWin = 1000;
    public int currentScore = 0;
    int totalScore = 0;
    
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
        if (!gameController) // not necessary for the moment
        {
            Debug.LogWarning("GameController is missing");
        }
    }
    
    void Update()
    {
        if (!player || !scoreText || !gameController)
        {
            return;
        }
    }
    
    public void AddToScore(int points)
    {
        currentScore += points;
        totalScore += points;
        UpdateScoreText();
        player.circleRadius += points * player.radiusIncrementValue;
        player.UpdateText(currentScore);
    }

    public void RemoveFromScore(int points)
    {
        currentScore += points;
        player.circleRadius += points * player.radiusIncrementValue;
        player.UpdateText(currentScore);
    }

    private void UpdateScoreText() //Score in the upper-left corner (total score) 
    {
        string text = totalScore.ToString();
        while (text.Length < textMesh.text.Length) // makes sure the score conserves all the 0's in front of it
        {
            text = "0" + text;
        }
        textMesh.text = text;
    } 

}
