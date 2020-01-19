using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Specific")] public int attackDamage = 1;
    public int enemyWeight = 1;
    public NavMeshAgent navMeshAgent;

    public int enemyHealth;

    [HideInInspector] public EnemySpawner enemySpawner;
    private int currentTreeSocketIndex = -1;
    private int targetTreeSocketIndex = -1;
    private TreeStatus target;

    private Animator animator;

    // pressed alt + enter / JetBrains' intelligence
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        animator = GetComponentInChildren<Animator>();
        if (!animator)
            Debug.LogError("Didn't find animator");
        // if (treeSockets.Length > 0)
        // {
        //     navMeshAgent.SetDestination(treeSockets[0].transform.position);
        // }
        //SetNewTarget();
    }

    public void EnemyCallback(string command)
    {
        print($"Enemy update {command}");
        if (animator == null || !animator.GetBool(IsAttacking))
        {
            SetNewTarget();
        }
    }

    IEnumerator Attack()
    {
        target.AttackWeight += enemyWeight;
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

    void EnemyHit()
    {
    }

    void EnemyDeath()
    {
        if (target)
            target.AttackWeight -= enemyWeight;
    }

    void SetNewTarget()
    {
        Dictionary<int, GameObject> ordinalTreeDistanceDict = new Dictionary<int, GameObject>();
        int lowestIndex = Int32.MaxValue;
        // print($"Number of treeSockets: {treeSockets.Length}");
        foreach (GameObject treeSocket in enemySpawner.treeSockets)
        {
            //print($"Treesocket: {treeSocket}");
            TreeStatus treeStatus = treeSocket.transform.GetComponentInChildren<TreeStatus>();
            if (treeStatus == null)
                continue;

            int index = treeSocket.GetComponent<TreeSocketIndex>().socketIndex;

            if (index <= currentTreeSocketIndex)
                continue;

            ordinalTreeDistanceDict.Add(index, treeSocket);
            // print($"Adding {treeSocket} {index}");
            // print(index + " " + lowestIndex);
            lowestIndex = Mathf.Min(index, lowestIndex);
        }

        targetTreeSocketIndex = lowestIndex;
        print("setting new destination" + lowestIndex);
        navMeshAgent.SetDestination(ordinalTreeDistanceDict[lowestIndex].transform.position);
    }

    // void SetNewTarget()
    // {
    //     float shortestDistance = Mathf.Infinity;
    //     Transform potentialTarget;
    //     print($"Number of treeSockets: {treeSockets.Length}");
    //     foreach (GameObject treeSocket in treeSockets)
    //     {
    //         print($"Treesocket: {treeSocket}");
    //         TreeStatus treeStatus = treeSocket.transform.GetComponentInChildren<TreeStatus>();
    //         if (treeStatus == null)
    //             continue;
    //         navMeshAgent.SetDestination(treeSocket.transform.position);
    //         shortestDistance = Math.Min(navMeshAgent.remainingDistance, shortestDistance);
    //         print($"Distance: {shortestDistance}");
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
        // if not dead tree, keep going
        if (!other.transform.CompareTag("TreeEncapsulator"))
            return;

        target = other.transform.parent.GetComponentInChildren<TreeStatus>();
        if (target == null)
            return;
        TreeSocketIndex treeSocketIndex = other.transform.GetComponentInParent<TreeSocketIndex>();
        if (treeSocketIndex == null)
        {
            Debug.LogError("no index found");
            return;
        }

        if (treeSocketIndex.socketIndex == targetTreeSocketIndex)
        {
            currentTreeSocketIndex = targetTreeSocketIndex;
            navMeshAgent.isStopped = true;
            StartCoroutine(Attack());
        }
    }
}