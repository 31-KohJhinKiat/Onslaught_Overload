using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour
{
    //Enemy health
    public int enemy1Health;

    //Enemy AI
    public NavMeshAgent Enemy1;
    public Transform player;
    //private bool canMove;
    public float DamageRate;
    private bool canAttack;
    public float TimeBetweenAttacks;
    private float currentAttackTime1 = 0.0f;

    //Sounds
    public AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip punchSound;
    public AudioClip damageSound;
    public AudioClip explosionSound;

    //Animation
    private Animator animator;

    //explosion
    public ParticleSystem Explosion;

    // Start is called before the first frame update
    void Start()
    {
        Enemy1 = GetComponent<NavMeshAgent>();
        //canMove = false;
        canAttack = false;
        Enemy1.isStopped = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        ExplosionOff();
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
            Enemy1.isStopped = false;

            if (canAttack == false)
            {
                print("moving");
                //canMove = true;
                //audioSource.PlayOneShot(walkSound);
                Enemy1.SetDestination(player.position);
                animator.SetBool("isMoving", true);
            }
            
        }

        /*if (canMove == true)
        {
            Enemy1.SetDestination(player.position);
        } */

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            audioSource.PlayOneShot(damageSound);
            enemy1Health--;
            Destroy(collision.gameObject);

            if (enemy1Health <= 0)
            {
                audioSource.PlayOneShot(explosionSound);
                StartCoroutine(dyingSecond());                           
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


    public void ExplosionOn()
    {
        Explosion.Play();
    }
    public void ExplosionOff()
    {
        Explosion.Stop();
    }
    IEnumerator dyingSecond()
    {
        //canMove = false;
        ExplosionOn();       
        yield return new WaitForSeconds(2.3f);
        Destroy(gameObject);

    }

}
