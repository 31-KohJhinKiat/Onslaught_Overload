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
        canAttack = false;
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
            Enemy1.isStopped = true;
            animator.SetBool("isMoving", false);
            return;
        }
        else if (GameManager.instance.pause == false)
        {
            currentAttackTime1 = currentAttackTime1 + Time.deltaTime;

            if (canAttack == false)
            {
                print("moving");
                Enemy1.SetDestination(player.position);
                //audioSource.PlayOneShot(walkSound);
                animator.SetBool("isMoving", true);
            }
            
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            enemy1Health--;
            Destroy(collision.gameObject);

            if (enemy1Health <= 0)
            {
                Destroy(gameObject);
                
            }
        }
    }


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("Player") )
        {
            animator.SetBool("isMoving", false);
            canAttack = true;

            if (currentAttackTime1 >= TimeBetweenAttacks)
            {
                currentAttackTime1 = 0;
                GameManager.instance.MinusHealth(DamageRate);                              
                animator.SetTrigger("isAttacking");
                audioSource.PlayOneShot(punchSound);              

            }
            
            
        }
       

    }

    private void OnCollisionExit(Collision collision)
    {
        canAttack = false;
    }



}
