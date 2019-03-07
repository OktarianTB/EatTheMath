using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    [Range(2f, 5f)] public float inputVelocity = 3f;

    Rigidbody2D playerRigidbody;
    Player playerCircle;

    bool playerFirstInputDone = false;
    float halfHeight;
   
    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerCircle = FindObjectOfType<Player>();
        halfHeight = Camera.main.orthographicSize;

        if (!player)
        {
            Debug.LogWarning("The Player game object is missing.");
            return;
        }
        if (!player.GetComponent<Rigidbody2D>())
        {
            Debug.LogWarning("The Player game object doesn't have a rigidbody");
        }
        if (!playerCircle)
        {
            Debug.LogWarning("The Player Circle Class is missing");
        }
    }
    
    void Update()
    {
        if (!player || !playerRigidbody || !playerCircle)
        {
            return;
        }

        PlayerInput();
        CheckIfPlayerInBoundaries();
    }

    private void CheckIfPlayerInBoundaries()
    {
        float playerRadius = playerCircle.GetRadius();
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
    }
}
