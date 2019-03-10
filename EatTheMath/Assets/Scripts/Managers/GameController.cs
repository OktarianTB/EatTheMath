using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject lostPanel;

    Rigidbody2D playerRigidbody;
    ScoreManager scoreManager;
    Player playerScript;
    Vector2 stopMovingPos;

    bool playerFirstInputDone = false;
    bool gameIsFinished = false;
    public bool gameIsActive = true;
    public float halfHeight;
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
        if (!pausePanel)
        {
            Debug.LogWarning("Pause panel is missing from Game Controller game object");
        }
        else
        {
            pausePanel.gameObject.SetActive(false);
        }
        if (!lostPanel)
        {
            Debug.LogWarning("Lost panel is missing from Game Controller game object");
        }
        else
        {
            lostPanel.gameObject.SetActive(false);
        }
        if (!winPanel)
        {
            Debug.LogWarning("Win panel is missing from Game Controller game object");
        }
        else
        {
            winPanel.gameObject.SetActive(false);
        }
    }
    
    void Update()
    {
        if (!player || !playerRigidbody || !playerScript ||!scoreManager ||!pausePanel ||!lostPanel ||!winPanel)
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
            ManagePlayerVelocity();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !gameIsFinished)
        {
            ManagePauseState();
        }
    }

    private void ManagePlayerVelocity()
    {
        if (!playerFirstInputDone)
        {
            playerFirstInputDone = true;
        }
        float randomVelocity = UnityEngine.Random.Range(minVelocity, maxVelocity);
        playerRigidbody.velocity = new Vector2(0f, randomVelocity);
    }

    public void ManagePauseState()
    {
        if (gameIsActive)
        {
            stopMovingPos = player.transform.position;
            playerFirstInputDone = false;
        }
        gameIsActive = !gameIsActive;
        pausePanel.gameObject.SetActive(!gameIsActive);
    }

    private void ManagePlayerMovement()
    {
        if (!playerFirstInputDone || !gameIsActive)
        {
            player.transform.position = stopMovingPos;
        }
    }

    public void CheckState(int score) //Detect wether the player has lost, has won or continue playing
    {
        if(score >= scoreManager.scoreToWin)
        {
            winPanel.gameObject.SetActive(true);
            ManageEndOfGame();
        }
        else if (score < 0) 
        {
            lostPanel.gameObject.SetActive(true);
            ManageEndOfGame();
        }
    }

    private void ManageEndOfGame()
    {
        gameIsFinished = true;
        DestroyAllTargets();
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
