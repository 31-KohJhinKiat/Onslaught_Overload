using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProtoEnemyScript : MonoBehaviour
{
    //Enemy health
    public int enemy1Health;

    //Search for player
    public NavMeshAgent Enemy1;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float TimeBetweenAttacks;
    bool alreadyAttacked;
    public float contactDistance;
    public float damageRate;

    //States
    public float SightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy1.SetDestination(player.position);
        PlayerInSightRange = Physics.CheckSphere(transform.position,
            SightRange, WhatIsPlayer);
        PlayerInAttackRange = Physics.CheckSphere(transform.position,
            AttackRange, WhatIsPlayer);

        if (!PlayerInSightRange && !PlayerInAttackRange)
        {
            Patroling();
        }

        if (PlayerInSightRange && !PlayerInAttackRange)
        {
            Chaseplayer();
        }

        if (PlayerInSightRange && PlayerInAttackRange)
        {
            AttackPlayer();
        }

    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy1 = GetComponent<NavMeshAgent>();
        Enemy1.isStopped = false;
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            Enemy1.SetDestination(walkPoint);
        }

        Vector3 DistanceToWalkPoint = transform.position - walkPoint;

        //Walk point reached
        if (DistanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX,
            transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void Chaseplayer()
    {
        Enemy1.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Enemy does not move
        Enemy1.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Punch

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), TimeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        enemy1Health -= damage;

        if (enemy1Health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 1f);
        }

    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
