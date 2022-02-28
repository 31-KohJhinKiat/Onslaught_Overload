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
    private GameObject player;
    //private bool canMove;
    private bool canAttack;
    public float TimeBetweenAttacks;
    private float currentAttackTime2 = 0.0f;

    //Field of view
    public float radius;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    //disappear
    private BoxCollider boxCollider;
    //private SkinnedMeshRenderer meshRenderer;

    //Sounds
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip shootSound;
    public AudioClip explosionSound;

    //Weapon
    public GameObject enemyBullet;
    public GameObject cannon;

    //explosion
    public ParticleSystem Explosion;

    // Start is called before the first frame update
    void Start()
    {
        Enemy2 = GetComponent<NavMeshAgent>();
        boxCollider = GetComponent<BoxCollider>();        
        player = GameObject.Find("FPSController(Alex)");
        //canMove = false;
        Enemy2.isStopped = false;
        audioSource = GetComponent<AudioSource>();
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
            //Enemy2.isStopped = true;
            //Enemy2.enabled = false;
            return;
        }
        else
        {

            if (canSeePlayer == true && Enemy2.enabled == true)
            {
                currentAttackTime2 = currentAttackTime2 + Time.deltaTime;
                Enemy2.isStopped = false;
                Enemy2.SetDestination(player.transform.position);
                
            }
            


            //Shoot player
            if (currentAttackTime2 >= TimeBetweenAttacks)
            {
                currentAttackTime2 = 0;
                Instantiate(enemyBullet, cannon.transform.position,
                cannon.transform.rotation);
                audioSource.PlayOneShot(shootSound);
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
            enemy2Health--;
            Destroy(collision.gameObject);

            //enemy die
            if (enemy2Health <= 0)
            {
                
                StartCoroutine(dyingSecond());
            }
        }
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
        print("explode 2");
        boxCollider.enabled = false;
        audioSource.PlayOneShot(explosionSound);
        Enemy2.enabled = false;
        ExplosionOn();
        yield return new WaitForSeconds(2.3f);
        Destroy(gameObject);

    }

}
