using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Specific")]
    public int attackDamage = 1;
    public int enemyWeight = 1;
    public NavMeshAgent navMeshAgent;
    public int enemyHealth;
    private TreeStatus target;
    private GameObject[] treeSockets;
    private Animator animator;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");


    private void Start()
    {
        FindTreeSockets();
        animator = GetComponentInChildren<Animator>();
        if (!animator)
            Debug.LogError("Didn't find animator");
        if (treeSockets.Length > 0)
        {
            navMeshAgent.SetDestination(treeSockets[0].transform.position);
        }
    }

    void FindTreeSockets()
    {
        treeSockets = GameObject.FindGameObjectsWithTag("TreeSocket");
    }

    IEnumerator Attack()
    {
        target.attackWeight += enemyWeight;
        WaitForSeconds waitOneSecond = new WaitForSeconds(1);
        animator.SetBool(IsAttacking, true);
        while (target)
        {
            target.HitTree(attackDamage);
            yield return waitOneSecond;
        }

        animator.SetBool(IsAttacking, false);

        SetNewTarget();
        navMeshAgent.isStopped = false;
    }

    void EnemyDeath()
    {
        if (target)
            target.attackWeight -= enemyWeight;
    }

    void SetNewTarget()
    {
        float shortestDistance = Mathf.Infinity;
        Transform potentialTarget;
        print($"Number of treeSockets: {treeSockets.Length}");
        foreach (GameObject treeSocket in treeSockets)
        {
            print($"Treesocket: {treeSocket}");
            TreeStatus treeStatus = treeSocket.transform.GetComponentInChildren<TreeStatus>();
            if (treeStatus == null)
                continue;
            navMeshAgent.SetDestination(treeSocket.transform.position);
            shortestDistance = Math.Min(navMeshAgent.remainingDistance, shortestDistance);
            print($"Distance: {shortestDistance}");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if not dead tree, keep going
        if (!other.transform.CompareTag("TreeEncapsulator"))
            return;

        navMeshAgent.isStopped = true;

        target = other.transform.parent.GetComponentInChildren<TreeStatus>();
        if (target == null)
            Debug.LogError("treeStatus not found");
        StartCoroutine(Attack());
    }
}