using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public GameObject pinePrefab;
    public GameObject birchPrefab;
    public GameObject cherryPrefab;
    [HideInInspector] public GameObject currentTreeEncapsulator;
    [HideInInspector] public GameObject currentDeadTree;
    public GameObject menu;

    public void TurretSelector(string turret)
    {
        switch (turret)
        {
            case ("Pine"):
                AssignTree(pinePrefab);
                break;
            case ("Birch"):
                AssignTree(birchPrefab);
                break;
            case ("Cherry"):
                AssignTree(cherryPrefab);
                break;
            default:
                print("Unknown turret");
                return;
        }

        currentDeadTree.gameObject.SetActive(false);
        currentTreeEncapsulator.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }

    private void AssignTree(GameObject treePrefab)
    {
        
        GameObject spawnedTree = Instantiate(treePrefab, currentDeadTree.transform);
        if (spawnedTree == null)
            Debug.LogError("Tree failed to spawn");
        spawnedTree.transform.SetParent(currentDeadTree.transform.parent);
        TreeStatus treeStatus = spawnedTree.GetComponent<TreeStatus>();
        if (treeStatus == null)
            Debug.LogError("No treeStatus found");
        treeStatus.deadTree = currentDeadTree;
        treeStatus.treeEncapsulator = currentTreeEncapsulator;
    }
}