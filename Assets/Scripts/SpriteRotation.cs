using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotation : MonoBehaviour {
    private GameObject mainCamera;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        if(mainCamera == null)
            Debug.LogError("Failed to get camera");
    }

    void Update() {
        transform.LookAt(transform.position + mainCamera.transform.rotation *
        Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
