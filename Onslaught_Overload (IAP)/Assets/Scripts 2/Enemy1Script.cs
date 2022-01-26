using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour
{


    //Search for player
    public NavMeshAgent Enemy1;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

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
    public float sightRange, AttackRange;
    public bool PlayerInSightRange, PlayerInAttackRange;

    //Animation
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy1.SetDestination(player.position);

    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy1 = GetComponent<NavMeshAgent>();
        Enemy1.isStopped = false;
    }

    private void Patroling()
    {

    }

    private void Chaseplayer()
    {

    }

    private void AttackPlayer()
    {

    }

}
