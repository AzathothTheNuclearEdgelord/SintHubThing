using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStatus : MonoBehaviour
{
    public int health;
    [HideInInspector] public GameObject deadTree;
    [HideInInspector] public GameObject treeEncapsulator;
    private int attackWeight;
    private EnemySpawner enemySpawner;

    public int AttackWeight
    {
        get => attackWeight;
        set => AttackWeightChange(value); 
        // set => attackWeight = value; 
        
    }

    int AttackWeightChange(int value)
    {
        attackWeight += value;
        // enemySpawner.RequestUpdate("Attack weight changed");
        return value;
    }

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.RequestUpdate("It's alive!");
    }

    public void HitTree(int dmgValue)
    {
        health -= dmgValue;
        if (health <= 0)
        {
            print("I'M DEAD");
            enemySpawner.RequestUpdate("WHYYYY!");
            deadTree.SetActive(true);
            treeEncapsulator.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            HitTree(1000);
        }
    }
}
