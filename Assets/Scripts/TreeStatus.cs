using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStatus : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public Light light;
    public float lightIntensityMultiplier = 20;

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
        if (maxHealth == 0)
        {
            Debug.LogError("MaxHealth should not be zero!");
        }
        currentHealth = maxHealth;
        print(maxHealth);

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

    
    private void Update()
    {
        float intensity = ((float)currentHealth / (float)maxHealth) * (float)lightIntensityMultiplier;
        light.intensity = intensity;
    }

    public void HitTree(int dmgValue)
    {
        currentHealth -= dmgValue;
        if (currentHealth <= 0)
        {
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
}