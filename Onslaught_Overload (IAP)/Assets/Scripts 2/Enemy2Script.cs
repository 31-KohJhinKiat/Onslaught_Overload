using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy2Script : MonoBehaviour
{
    //Enemy health
    public int enemy2Health;

    //Enemy AI
    public NavMeshAgent Enemy2;
    public Transform player;
    //private bool canMove;
    public float DamageRate;
    private bool canAttack;
    public float contactDistance;
    public float TimeBetweenAttacks;
    private float currentAttackTime1 = 0.0f;

    //Sounds
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip punchSound;
    public AudioClip shootSound;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        Enemy2 = GetComponent<NavMeshAgent>();
        //canMove = false;
        canAttack = false;
        Enemy2.isStopped = false;
        audioSource = GetComponent<AudioSource>();
        //ExplosionOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
