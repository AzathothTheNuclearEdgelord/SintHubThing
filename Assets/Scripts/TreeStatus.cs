using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStatus : MonoBehaviour
{
    public GameObject deadTree;
    public int health;
    public int attackWeight;

    public void HitTree(int dmgValue)
    {
        print("That hurt!" + health);
        print(dmgValue);
        
        health -= dmgValue;
        if (health <= 0)
        {
            print("I'M DEAD");
            deadTree.SetActive(true);
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
