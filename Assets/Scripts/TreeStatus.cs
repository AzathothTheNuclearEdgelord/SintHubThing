using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStatus : MonoBehaviour
{
    public int health;

    [Tooltip("If this is the base tree set this to true")]
    public bool isBaseTree;

    [HideInInspector] public GameObject deadTree;
    [HideInInspector] public GameObject treeEncapsulator;
    private int attackWeight;
    private EnemySpawner enemySpawner;
    public GameObject finishPanel;

    public int AttackWeight
    {
        get => attackWeight;
        set => AttackWeightChange(value);
        // set => attackWeight = value; 
    }

    int AttackWeightChange(int value)
    {
        attackWeight += value;
        enemySpawner.RequestUpdate("Attack weight changed");
        return value;
    }

    private void Start()
    {
        if (isBaseTree)
        {
            finishPanel = GameObject.Find("GameFinish");
            if (finishPanel == null)
                Debug.LogError("Finish panel not found");
            finishPanel.SetActive(false);
        }

        enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner == null)
            Debug.LogError("enemySpawner not found");
        enemySpawner.RequestUpdate("It's alive!");
    }

    public void HitTree(int dmgValue)
    {
        health -= dmgValue;
        if (health <= 0)
        {
            print("I'M DEAD");
            enemySpawner.RequestUpdate("WHYYYY!");
            if (isBaseTree)
            {
                finishPanel.SetActive(true);
                Transform loseText = finishPanel.transform.Find("Lose");
                if (loseText == null)
                    Debug.LogError("Lose text not found");

                loseText.gameObject.SetActive(true);
            }
            else
            {
                deadTree.SetActive(true);
                treeEncapsulator.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    // private int filter = 0;

    private void Update()
    {
        // if (filter++ % 60 == 0)
        // {
        //     print($"weight: {attackWeight}");
        // }
        if (Input.GetKeyDown(KeyCode.K))
        {
            HitTree(1000);
        }
    }
}