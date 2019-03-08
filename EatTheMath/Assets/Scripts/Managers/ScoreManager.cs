using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    Player player;
    public GameObject scoreText;
    TextMeshProUGUI textMesh;
    public int currentScore = 0;
    int totalScore = 0;
    
    void Start()
    {
        player = FindObjectOfType<Player>();
        textMesh = scoreText.GetComponent<TextMeshProUGUI>();
        if (!player)
        {
            Debug.LogWarning("Player Script is missing");
        }
        if (!scoreText)
        {
            Debug.LogWarning("ScoreText Game Object is missing");
        }
    }
    
    void Update()
    {
        if (!player || !scoreText)
        {
            return;
        }
    }
    
    public void AddToScore(int points)
    {
        currentScore += points;
        totalScore += points;
        textMesh.text = totalScore.ToString();
        player.circleRadius += points * player.radiusIncrementValue;
        player.UpdateText(Mathf.Clamp(currentScore, 0, 1000));
    }

    public void RemoveFromScore(int points)
    {
        currentScore -= points;
        player.circleRadius -= points * player.radiusIncrementValue;
        player.UpdateText(Mathf.Clamp(currentScore, 0, 1000));
    }

}
