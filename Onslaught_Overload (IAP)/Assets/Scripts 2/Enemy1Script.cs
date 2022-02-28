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
    private GameObject player;
    public float DamageRate;
    private bool canAttack;
    public float TimeBetweenAttacks;
    private float currentAttackTime1 = 0.0f;

    //Field of view
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    //disappear
    private BoxCollider boxCollider;
    private SkinnedMeshRenderer meshRenderer;

    //Sounds
    public AudioSource audioSource;
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
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        player = GameObject.Find("FPSController(Alex)");      
        canAttack = false;
        Enemy1.isStopped = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        StartCoroutine(FOVRoutine());
        
    }

    private void Awake()
    {
        ExplosionOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause == true || GameManager.instance.GetIsGameOver())
        {
            
            canAttack = false;
            Enemy1.isStopped = true;
            animator.SetBool("isMoving", false);
            return;
        }
        else
        {
            //attack time
            currentAttackTime1 = currentAttackTime1 + Time.deltaTime;
            
            

            if (canAttack == false)
            {


                if (canSeePlayer == true && Enemy1.enabled == true)
                {
                    Enemy1.isStopped = false;
                    Enemy1.SetDestination(player.transform.position);
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    animator.SetBool("isMoving", false);
                }

            }
            else
            {
                Enemy1.isStopped = true;
            }
  
        }

    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks =
            Physics.OverlapSphere(transform.position,
            radius, targetMask);


        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position -
                transform.position).normalized;


            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget =
                    Vector3.Distance(transform.position, target.position);
                

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    print("hunt");
                }
                else
                {
                    canSeePlayer = false;
                    
                }
            }
            else
            {
                canSeePlayer = false;

            }

        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
            
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            //damage
            audioSource.PlayOneShot(damageSound);
            enemy1Health--;
            Destroy(collision.gameObject);

            //enemy die
            if (enemy1Health <= 0)
            {
   
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
        boxCollider.enabled = false;
        meshRenderer.enabled = false;
        audioSource.PlayOneShot(explosionSound);
        Enemy1.enabled = false;
        ExplosionOn();       
        yield return new WaitForSeconds(2.3f);
        Destroy(gameObject);

    }

}
