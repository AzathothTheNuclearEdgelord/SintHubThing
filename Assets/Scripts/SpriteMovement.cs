using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMovement : MonoBehaviour {
    public Camera mainCamera;
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.LookAt(transform.position + mainCamera.transform.rotation *
        Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
