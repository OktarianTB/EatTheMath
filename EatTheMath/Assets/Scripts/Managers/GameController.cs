using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    Rigidbody2D playerRigidbody;
    ScoreManager scoreManager;
    Player playerScript;

    bool playerFirstInputDone = false;
    float halfHeight;
    float inputVelocity = 4.5f;

    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerScript = FindObjectOfType<Player>();
        halfHeight = Camera.main.orthographicSize;
        scoreManager = FindObjectOfType<ScoreManager>();

        if (!player)
        {
            Debug.LogWarning("The Player game object is missing");
            return;
        }
        if (!player.GetComponent<Rigidbody2D>())
        {
            Debug.LogWarning("The Player game object doesn't have a rigidbody");
        }
        if (!playerScript)
        {
            Debug.LogWarning("The Player Circle Class is missing");
        }
        if (!scoreManager)
        {
            Debug.LogWarning("The Score Manager is missing");
        }
    }
    
    void Update()
    {
        if (!player || !playerRigidbody || !playerScript ||!scoreManager)
        {
            return;
        }

        PlayerInput();
        CheckIfPlayerInBoundaries();
    }

    private void CheckIfPlayerInBoundaries()
    {
        float playerRadius = playerScript.GetRadius();
        if(player.transform.position.y <= (-halfHeight + playerRadius)){
            playerRigidbody.velocity = new Vector2(0f, 3f);
        } else if(player.transform.position.y >= (halfHeight - playerRadius))
        {
            playerRigidbody.velocity = new Vector2(0f, -2f);
        }
    }

    private void PlayerInput()
    {
        if (!playerFirstInputDone)
        {
            player.transform.position = new Vector2(player.transform.position.x, 0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!playerFirstInputDone)
            {
                playerFirstInputDone = true;
            }
            playerRigidbody.velocity = new Vector2(0f, inputVelocity);
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            scoreManager.AddToScore(50);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            scoreManager.RemoveFromScore(75);
        }
    }

    public void CheckState(int score) //Detect wether the player has lost, has won or continue playing
    {
        if(score >= scoreManager.scoreToWin)
        {
            print("Win");
        } else if (score < 0) 
        {
            print("Lost");
        }
    }

}
