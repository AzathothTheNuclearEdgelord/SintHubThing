using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public int hitStrength;
    public NavMeshAgent agent;
    private GameObject[] treeSockets;

    private void Start()
    {
        FindDeadTrees();
        if (treeSockets.Length > 0)
        {
            agent.SetDestination(treeSockets[0].transform.position);
        }
    }

    void FindDeadTrees()
    {
        treeSockets = GameObject.FindGameObjectsWithTag("TreeSocket");
    }
}
