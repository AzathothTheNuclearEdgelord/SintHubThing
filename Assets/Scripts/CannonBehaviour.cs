using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBehaviour : MonoBehaviour {
    private Transform target;
    public float range = 10f;
    public string enemyTag = "Enemy";

    float turnSpeed = 10;

    Vector3 startPos;
    Quaternion startRot;

    private void Start() {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies) {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range) {
            target = nearestEnemy.transform;
        } else {
            target = null;
        }
    }

    void Update() {
        if(target == null) {
            return;
        }
        
        //target lockon
        //transform.LookAt(target);
        Vector3 diretion = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(diretion);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);


    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
