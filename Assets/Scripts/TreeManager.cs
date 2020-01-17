using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turrets
{
    Pine,
    Cherry,
    Birch
}

public class TreeManager : MonoBehaviour
{
    public GameObject pinePrefab;
    public GameObject birchPrefab;
    public GameObject cherryPrefab;
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
        menu.gameObject.SetActive(false);
    }

    private void AssignTree(GameObject treePrefab)
    {
        
        GameObject bollocks = Instantiate(treePrefab, currentDeadTree.transform);
        if (bollocks == null)
            Debug.LogError("Tree failed to spawn");
        bollocks.transform.SetParent(currentDeadTree.transform.parent);
        TreeStatus treeStatus = bollocks.GetComponent<TreeStatus>();
        if (treeStatus == null)
            Debug.LogError("No treeStatus found");
        treeStatus.deadTree = currentDeadTree;
    }
}