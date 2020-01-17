using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public Transform spawnPoint;
    void Start()
    {
        Instantiate(ghostPrefab, spawnPoint);

    }

}
