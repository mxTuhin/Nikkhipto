using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    private float timeBetweenAttacks;
    bool alreadyAttacked;

    public float slightRange, atttackRange;
    public bool playerInSightRange, playerInAttackRange;
    public static EnemyAI instance;
    private Animator _animator;

    public GameObject muzzleFlash;
    public GameObject flashPosition;

    private Health _health;


    private void Awake()
    {
        _health = GetComponent<Health>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            print("Found Player");
        }
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            print("Nav Player");
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        timeBetweenAttacks = Random.Range(1, 2);
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_health.isDeadTrigger)
            return;
        playerInSightRange = Physics.CheckSphere(transform.position, slightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, atttackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) patrolling();
        if (playerInSightRange && !playerInAttackRange) chasePlayer();
        if (playerInAttackRange && playerInSightRange) attackPlayer();


    }
    private void patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        // if (walkPointSet)
        //     agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z+randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void chasePlayer()
    {
        // agent.SetDestination(player.position);

    }
    private void attackPlayer()
    {
        // agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Shoot();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    void Shoot()
    {
        _animator.SetTrigger("isShot");
        player.GetComponent<Health>().TakeDamage(10, "Player");
        Instantiate(muzzleFlash, flashPosition.transform.position, Quaternion.identity);
        
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
