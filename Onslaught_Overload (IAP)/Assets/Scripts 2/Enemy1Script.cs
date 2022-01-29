using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour
{
    //Enemy health
    public int enemy1Health;

    //Search for player
    public NavMeshAgent Enemy1;
    public Transform player;

    //Attacking
    public float DamageRate;
    private bool canAttack;
    public float contactDistance;
    public float TimeBetweenAttacks;
    private float currentAttackTime1 = 0.0f;

    //Sounds
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip punchSound;

    //Animation
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Enemy1 = GetComponent<NavMeshAgent>();
        Enemy1.isStopped = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause == true)
        {
            canAttack = false;
            animator.SetBool("isMoving", false);
            return;
        }
        else if (GameManager.instance.pause == false)
        {
            currentAttackTime1 = currentAttackTime1 + Time.deltaTime;
            Enemy1.SetDestination(player.position);
            //audioSource.PlayOneShot(walkSound);
            animator.SetBool("isMoving", true);
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player") && !canAttack)
        {
            animator.SetBool("isMoving", false);
            canAttack = true;

            if (currentAttackTime1 >= TimeBetweenAttacks)
            {
                currentAttackTime1 = 0;
                GameManager.instance.MinusHealth(DamageRate * Time.deltaTime);                              
                animator.SetTrigger("isAttacking");
                audioSource.PlayOneShot(punchSound);              
                canAttack = false;
            }
                

        }
    }

}
