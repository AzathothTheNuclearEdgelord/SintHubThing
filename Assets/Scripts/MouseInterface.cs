using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInterface : MonoBehaviour
{
    public GameObject menu;
    public Camera cam;
    public TreeManager treeManager;
    [HideInInspector] public GameObject currentDeadTree;
    [HideInInspector] public GameObject currentTreeEncapsulator;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("TreeSocket"))
                {
                    // treeManager.deadTree = hit.transform.gameObject.GetComponent();
                    GameObject colliderObject = hit.transform.gameObject;
                    currentDeadTree = colliderObject.transform.Find("DeadTree").gameObject;
                    currentTreeEncapsulator = colliderObject.transform.Find("TreeEncapsulator").gameObject;
                    if (currentDeadTree == null)
                        print("PANIC!");
                    else
                    {
                        // found dead tree
                        if (currentDeadTree.activeSelf)
                        {
                            treeManager.currentDeadTree = currentDeadTree;
                            treeManager.currentTreeEncapsulator = currentTreeEncapsulator;
                            menu.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}