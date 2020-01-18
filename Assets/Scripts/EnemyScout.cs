using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyScout : MonoBehaviour
{
    private GameObject[] treeSockets;
    public NavMeshAgent navMeshAgent;

    void Start()
    {
        treeSockets = GameObject.FindGameObjectsWithTag("TreeSocket");
        //StartCoroutine(SortTargets());
        SortTargets();
    }

    void SortTargets()
    {
        Dictionary<float, GameObject> treeDistanceDict = new Dictionary<float, GameObject>();

        //navMeshAgent.isStopped = true;
        Transform potentialTarget;
        print(message: $"Number of treeSockets: {treeSockets.Length}");
        foreach (GameObject treeSocket in treeSockets)
        {
            print(message: $"TreeSocket: {treeSocket}");
            navMeshAgent.SetDestination(target: treeSocket.transform.position);
            // while (navMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
            //     yield return true;
            treeDistanceDict.Add(key: navMeshAgent.remainingDistance, value: treeSocket);
            print($"Base tree distance: {navMeshAgent.remainingDistance}");
        }

        List<float> sortedList = new List<float>();
        foreach (float distance in treeDistanceDict.Keys)
        {
            sortedList.Add(item: distance);
        }

        List<GameObject> treeList = new List<GameObject>();
        
        sortedList.Sort();
        foreach (float distance in sortedList)
        {
            print(message: $"Distance: {distance}");
            treeList.Add(item: treeDistanceDict[key: distance]);
        }
        
    }

}
