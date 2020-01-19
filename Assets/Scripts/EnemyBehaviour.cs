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
    private TreeStatus attackingTarget;
    public bool inPersuit = false;

    private Animator animator;

    // pressed alt + enter / JetBrains' intelligence
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Start()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.stoppingDistance = 1.5f;
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

    public bool isRunning;

    private int forceRefresh = 0;
    private void Update()
    {
        isRunning = navMeshAgent.isStopped;
        
        if (forceRefresh++ % 60 == 0)
            SetNewTarget();
    }

    public bool localAttacking = false; 

    public void EnemyCallback(string command)
    {
        print($"Enemy update {command}");
        // if (!localAttacking)
        {
            inPersuit = false;
            SetNewTarget();
        }
    }

    IEnumerator Attack()
    {
        attackingTarget.AttackWeight += enemyWeight;
        WaitForSeconds waitOneSecond = new WaitForSeconds(1);
        animator.SetBool(IsAttacking, true);
        localAttacking = true;
        while (attackingTarget)
        {
            attackingTarget.HitTree(attackDamage);
            yield return waitOneSecond;
        }

        animator.SetBool(IsAttacking, false);
        localAttacking = false;
        currentTreeSocketIndex = -1;
        inPersuit = false;
        
        SetNewTarget();
        navMeshAgent.isStopped = false;
    }

    public void EnemyHit(int damageValue)
    {
        enemyHealth -= damageValue;
        if (enemyHealth <= 0)
        {
            EnemyDeath();
        }
    }

    void AbandonAttack()
    {
        inPersuit = false;
        enemySpawner.UpdateEvent -= EnemyCallback;
        StopCoroutine(Attack());

        if (attackingTarget)
            attackingTarget.AttackWeight -= enemyWeight;
        
    }
    
    void EnemyDeath()
    {
        print("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARRGH");
        AbandonAttack();
        Destroy(gameObject);
    }

    public int peekIndex = -1;
    
    void SetNewTarget()
    {
        // if not yet, initialised return.
        if (enemySpawner == null || enemySpawner.treeSockets == null)
            return;
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

            // if (index <= currentTreeSocketIndex)
            //     continue;

            ordinalTreeDistanceDict.Add(index, treeSocket);
            // print($"Adding {treeSocket} {index}");
            // print(index + " " + lowestIndex);
            lowestIndex = Mathf.Min(index, lowestIndex);
        }

        if (lowestIndex == Int32.MaxValue)
        {
            Debug.LogWarning("No new target found");
            inPersuit = false;
        }
        else
        {
            targetTreeSocketIndex = lowestIndex;
            // print("setting new destination" + lowestIndex);
            navMeshAgent.SetDestination(ordinalTreeDistanceDict[lowestIndex].transform.position);
            peekIndex = lowestIndex;
            inPersuit = true;
            navMeshAgent.isStopped = false;

        }
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

        attackingTarget = other.transform.parent.GetComponentInChildren<TreeStatus>();
        if (attackingTarget == null)
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
            attackingTarget.AttackWeight += enemyWeight;
            StartCoroutine(Attack());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if(other.transform.CompareTag("TreeEncapsulator"))
            AbandonAttack();
            
    }
}