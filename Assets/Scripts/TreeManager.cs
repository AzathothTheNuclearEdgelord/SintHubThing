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
    [HideInInspector]
    public GameObject currentDeadTree;
    public GameObject menu;
    
    public void TurretSelector(string turret)
    {
        switch (turret)
        {
            case ("Pine"):
                Instantiate(pinePrefab, currentDeadTree.transform);
                break;
            case ("Birch"):
                Instantiate(birchPrefab, currentDeadTree.transform);
                break;
            case ("Cherry"):
                Instantiate(cherryPrefab, currentDeadTree.transform);
                break;
            default:
                print("Unknown turret");
                return;
        }
        currentDeadTree.gameObject.SetActive(false);
        menu.gameObject.SetActive(false);
        
    }
}
