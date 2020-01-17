using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStatus : MonoBehaviour
{
    public GameObject deadTree;
    public int health;

    public void Hit(int dmgValue)
    {
        health -= dmgValue;
        if (health <= 0)
        {
            deadTree.SetActive(true);
            Destroy(gameObject);
        }
    }
    
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.K))
    //     {
    //         Hit(1000);
    //     }
    // }
}
