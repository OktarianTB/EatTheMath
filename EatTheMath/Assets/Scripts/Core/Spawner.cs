using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject baseTarget;
    GameObject spawnedTarget;
    GameObject baseTargetCircle;
    GameObject baseTargetText;

    float minTargetScale = 1.5f;
    float maxTargetScale = 2.5f;
    int posMinValue = 5;
    int posMaxValue = 25;
    int negMinValue = -40;
    int negMaxValue = -10;

    public Color[] posColorArray;
    public Color[] negColorArray;

    void Start()
    {
        if (!baseTarget)
        {
            Debug.LogWarning("Target prefab is missing");
            return;
        }
        SpawnTargetShape();
    }

    void Update()
    {
        if (!baseTarget)
        {
            return;
        }
    }

    public void SpawnTargetShape()
    {
        if (!baseTarget)
        {
            return;
        }
        spawnedTarget = Instantiate(baseTarget, transform.position, Quaternion.identity);
        baseTargetCircle = spawnedTarget.transform.Find("TargetCircle").gameObject;
        baseTargetText = spawnedTarget.transform.Find("TargetValueText").gameObject;

        float randomScale = Random.Range(minTargetScale, maxTargetScale);
        baseTargetCircle.transform.localScale = new Vector2(randomScale, randomScale);

        float posOrNegRandom = Random.Range(0, 2);
        int randomColor = Random.Range(0, posColorArray.Length - 1); // assumes that both arrays are the same length
        SpriteRenderer spriteRd = baseTargetCircle.GetComponent<SpriteRenderer>();
        TextMeshPro targetTextMesh = baseTargetText.GetComponent<TextMeshPro>();
        
        if (posOrNegRandom == 0) // circle is positive type
        {
            string randomValue = Random.Range(posMinValue, posMaxValue).ToString();
            targetTextMesh.text = randomValue;
            spriteRd.color = posColorArray[randomColor];
        }
        else if (posOrNegRandom == 1) // circle is negative type
        {
            string randomValue = Random.Range(negMinValue, negMaxValue).ToString();
            targetTextMesh.text = randomValue;
            spriteRd.color = negColorArray[randomColor];
        } else
        {
            Debug.LogWarning("posOrNegRandom isn't 0 or 1");
        }
    }

}
