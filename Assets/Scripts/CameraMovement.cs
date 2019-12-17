using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float velocity = 10f;
    Vector3 startPos = new Vector3(0, 15, 0);
    Vector3 reuseVector;

    Vector3 movement;

    void Start() {
        transform.position = startPos;
    }

    void Update() {
        PlayerMovement();

    }

    void PlayerMovement() {
        float distance = Time.deltaTime * velocity;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 verticalMovement = transform.forward * vertical;
        Vector3 rightMovement = transform.right * horizontal;
        movement = (verticalMovement + rightMovement) * distance;
        reuseVector = movement + transform.position;

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            reuseVector.y += 10;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            reuseVector.y -= 10;

        reuseVector.y = Mathf.Clamp(reuseVector.y, 15, 35);

        transform.position = reuseVector;

    }
}
