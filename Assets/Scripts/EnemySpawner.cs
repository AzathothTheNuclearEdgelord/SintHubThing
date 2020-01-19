using System;
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
    public GameObject finishPanel;
    [HideInInspector] public GameObject[] treeSockets;

    public delegate void EnemyUpdate(string command);

    WaitForSeconds waitSpawnInterval;

    public EnemyUpdate UpdateEvent;

    void Awake()
    {
        treeSockets = GameObject.FindGameObjectsWithTag("TreeSocket");
    }

    private void Start()
    {
        waitSpawnInterval = new WaitForSeconds(.5f);
        StartCoroutine(SpawnEnemies());
    }

    void SpawnEnemy(GameObject prefab)
    {
        EnemyBehaviour enemyBehaviour = Instantiate(prefab, spawnPoint).GetComponent<EnemyBehaviour>();
        UpdateEvent += enemyBehaviour.EnemyCallback;
    }

    IEnumerator SpawnWave(GameObject prefab, int waveSize)
    {
        for (int i = 0; i < waveSize; i++)
        {
            SpawnEnemy(prefab);
            yield return waitSpawnInterval;
        }
    }

    IEnumerator SpawnEnemies()
    {
        WaitForSeconds wavePause = new WaitForSeconds(10);
        // gentle startup
        yield return StartCoroutine(SpawnWave(ghostPrefab, 5));
        yield return wavePause;
        yield return StartCoroutine(SpawnWave(ghostPrefab, 10));
        
        StartCoroutine(WaitForWinState());
    }

    IEnumerator WaitForWinState()
    {
        // still alive 
        int enemiesLeft = 1;
        while (enemiesLeft > 0)
        {
            GameObject[] livingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesLeft = livingEnemies.Length;
            yield return true;
        }

        finishPanel.SetActive(true);
        Transform winText = finishPanel.transform.Find("Win");
        if (winText == null)
            Debug.LogError("Win text not found");
        winText.gameObject.SetActive(true);
    }

    public void RequestUpdate(string txt)
    {
        UpdateEvent?.Invoke(txt);
    }
}