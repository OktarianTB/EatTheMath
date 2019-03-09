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
    Vector2 stopMovingPos;

    bool playerFirstInputDone = false;
    public bool gameIsActive = true;
    float halfHeight;
    float minVelocity = 4f;
    float maxVelocity = 5.5f;

    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerScript = FindObjectOfType<Player>();
        scoreManager = FindObjectOfType<ScoreManager>();
        halfHeight = Camera.main.orthographicSize;
        stopMovingPos = player.transform.position;

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
        if(player.transform.position.y <= (-halfHeight + playerRadius))
        {
            playerRigidbody.velocity = new Vector2(0f, 4f);
        }
        else if(player.transform.position.y >= (halfHeight - playerRadius))
        {
            playerRigidbody.velocity = new Vector2(0f, -3f);
        }
    }

    private void PlayerInput()
    {
        ManagePlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!playerFirstInputDone)
            {
                playerFirstInputDone = true;
            }
            float randomVelocity = UnityEngine.Random.Range(minVelocity, maxVelocity);
            playerRigidbody.velocity = new Vector2(0f, randomVelocity);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stopMovingPos = player.transform.position;
            gameIsActive = !gameIsActive;
            if (gameIsActive)
            {
                playerFirstInputDone = false;
            }
        }
    }

    private void ManagePlayerMovement()
    {
        if (!playerFirstInputDone) // the player doesn't move until first input
        {
            player.transform.position = stopMovingPos;
        }
        if (!gameIsActive) // once the player has lost/won or pause, the player stays at his final position
        {
            player.transform.position = stopMovingPos;
        }
    }

    public void CheckState(int score) //Detect wether the player has lost, has won or continue playing
    {
        if(score >= scoreManager.scoreToWin)
        {
            Debug.Log("Win");
            DestroyAllTargets();
        }
        else if (score < 0) 
        {
            Debug.Log("Lost");
            DestroyAllTargets();
        }
    }

    private void DestroyAllTargets()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Target"))
        {
            Destroy(obj.gameObject);
        }
        gameIsActive = false;
        stopMovingPos = player.transform.position;
    }
}
