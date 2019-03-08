using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    Player player;
    TextMeshProUGUI textMesh;
    public GameObject scoreText;
    public int scoreToWin = 1000;
    public int currentScore = 0;
    int totalScore = 0;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
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
    }
    
    void Update()
    {
        if (!player || !scoreText ||!textMesh)
        {
            return;
        }
    }

    public void ManagePoints(int points)
    {
        currentScore += points;
        player.circleRadius += points * player.radiusIncrementValue;
        player.UpdateText(currentScore);
        if(points > 0)
        {
            totalScore += points;
            UpdateScoreText();
        }
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
