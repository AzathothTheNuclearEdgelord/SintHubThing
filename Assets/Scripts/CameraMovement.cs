using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float velocity = 10f;
    Vector3 startPos = new Vector3(0, 10, 0);
    Vector3 reuseVector;

    Vector3 movement;

    void Start() {
        transform.position = startPos;
    }

    void Update() {
        PlayerMovement();

    }



    float prevScrollWheel = 0;
    float timeStep = 0;
    void PlayerMovement() {
        float distance = Time.deltaTime * velocity;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 verticalMovement = transform.forward * vertical;
        Vector3 rightMovement = transform.right * horizontal;
        movement = (verticalMovement + rightMovement) * distance;
        reuseVector = movement + transform.position;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            reuseVector.y += 5;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            reuseVector.y -= 5;
            

        /*float readScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        float scrollWheel = 0;

        if (readScrollWheel > 0)
            scrollWheel = 5;
        if (readScrollWheel < 0)
            scrollWheel = -5;

        if (prevScrollWheel != scrollWheel) {
            timeStep = 0;
        }
        else {
            timeStep += 10f * Time.deltaTime;
            reuseVector.y += Mathf.Lerp(0, scrollWheel, timeStep);
            print(reuseVector.y);
        }
        prevScrollWheel = scrollWheel;*/


        reuseVector.y = Mathf.Clamp(reuseVector.y, 5, 15);

        transform.position = reuseVector;

    }
}
