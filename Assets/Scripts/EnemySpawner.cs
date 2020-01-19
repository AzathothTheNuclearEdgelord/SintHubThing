﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
// https://www.tutorialspoint.com/csharp/csharp_events.htm   
// namespace SampleApp {
//     public delegate string MyDel(string str);
// 	
//     class EventProgram {
//         event MyDel MyEvent;
// 		
//         public EventProgram() {
//             this.MyEvent += new MyDel(this.WelcomeUser);
//         }
//         public string WelcomeUser(string username) {
//             return "Welcome " + username;
//         }
//         static void Main(string[] args) {
//             EventProgram obj1 = new EventProgram();
//             string result = obj1.MyEvent("Tutorials Point");
//             Console.WriteLine(result);
//         }
//     }
// }


    public GameObject ghostPrefab;
    public GameObject spikeHeadPrefab;
    public GameObject dogPrefab;
    public Transform spawnPoint;
    [HideInInspector] public GameObject[] treeSockets;
    public delegate void EnemyUpdate(string command);

    public EnemyUpdate UpdateEvent;
    
    void Awake()
    {
        treeSockets = GameObject.FindGameObjectsWithTag("TreeSocket");
    
    }

    private void Start()
    {
        EnemyBehaviour enemyBehaviour = Instantiate(dogPrefab, spawnPoint).GetComponent<EnemyBehaviour>();
        UpdateEvent += enemyBehaviour.EnemyCallback;
    }

    public void RequestUpdate(string txt)
    {
        UpdateEvent?.Invoke(txt);

    }
    
    
    

}
