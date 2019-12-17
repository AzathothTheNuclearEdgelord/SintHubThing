using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour {
    public GameObject enemy;

    Vector3 startPos;
    Quaternion startRot;

    private void Awake() {
        //startRot = transform.rotation;
        //startPos = transform.position;
    }


    // Update is called once per frame
    void Update() {
        transform.LookAt(transform.position + enemy.transform.rotation *
        Vector3.forward, enemy.transform.rotation * Vector3.up);
    }
}
