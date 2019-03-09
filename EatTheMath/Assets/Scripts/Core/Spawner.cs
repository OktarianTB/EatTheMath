using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Spawner : MonoBehaviour
{
    public GameObject baseTarget;
    GameObject spawnedTarget;
    GameObject baseTargetCircle;
    GameObject baseTargetText;
    GameController gameController;

    float minTargetScale = 1.5f;
    float maxTargetScale = 2.5f;
    int posMinValue = 20;
    int posMaxValue = 80;
    int negMinValue = -150;
    int negMaxValue = -30;

    float timeBetweenSpawn = 3f;
    float timeCounter = 0;

    public Color[] posColorArray;
    public Color[] negColorArray;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        if (!baseTarget)
        {
            Debug.LogWarning("Target prefab is missing");
            return;
        }
        if (!gameController)
        {
            Debug.LogWarning("GameController is missing");
        }

        SpawnTargetShape();
    }

    void Update()
    {
        if (!baseTarget || !gameController)
        {
            return;
        }

        ManageSpawnTiming();
    }

    private void ManageSpawnTiming()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= timeBetweenSpawn && gameController.gameIsActive)
        {
            SpawnTargetShape();
            timeCounter = 0;
            if (timeBetweenSpawn > 1.25f)
            {
                timeBetweenSpawn -= 0.12f;
            }
        }
    }

    public void SpawnTargetShape()
    {
        if (!baseTarget)
        {
            return;
        }
        spawnedTarget = Instantiate(baseTarget, GetRandomPosition(), Quaternion.identity);
        baseTargetCircle = spawnedTarget.transform.Find("TargetCircle").gameObject;
        baseTargetText = spawnedTarget.transform.Find("TargetValueText").gameObject;

        if (!baseTargetCircle || !baseTargetText)
        {
            Debug.LogWarning("The base target circle or text child is missing on the main target prefab");
            return;
        }

        SetRandomScale();
        SetTypeOfTarget();
    }

    private void SetTypeOfTarget()
    {
        float posOrNegRandom = UnityEngine.Random.Range(0, 11);
        int randomColor = UnityEngine.Random.Range(0, posColorArray.Length - 1); // assumes that both arrays are the same length
        SpriteRenderer spriteRd = baseTargetCircle.GetComponent<SpriteRenderer>();
        TextMeshPro targetTextMesh = baseTargetText.GetComponent<TextMeshPro>();

        if (posOrNegRandom < 5) // circle is positive type
        {
            string randomValue = UnityEngine.Random.Range(posMinValue, posMaxValue).ToString();
            targetTextMesh.text = randomValue;
            spriteRd.color = posColorArray[randomColor];
        }
        else if (posOrNegRandom >= 5) // circle is negative type
        {
            string randomValue = UnityEngine.Random.Range(negMinValue, negMaxValue).ToString();
            targetTextMesh.text = randomValue;
            spriteRd.color = negColorArray[randomColor];
        }
    }

    private void SetRandomScale()
    {
        float randomScale = UnityEngine.Random.Range(minTargetScale, maxTargetScale);
        baseTargetCircle.transform.localScale = new Vector2(randomScale, randomScale);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPos = new Vector3(transform.position.x, UnityEngine.Random.Range(-3.75f, 3.75f) , 0f);
        return randomPos;
    }

}
