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
    private float currentAttackTime2 = 0.0f;

    //Sounds
    public AudioSource audioSource;
    public AudioClip hoverSound;
    public AudioClip damageSound;
    public AudioClip shootSound;
    public AudioClip explosionSound;

    //explosion
    public ParticleSystem Explosion;

    // Start is called before the first frame update
    void Start()
    {
        Enemy2 = GetComponent<NavMeshAgent>();
        //canMove = false;
        canAttack = false;
        Enemy2.isStopped = false;
        audioSource = GetComponent<AudioSource>();
        ExplosionOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause == true)
        {
            canAttack = false;
            Enemy2.isStopped = true;

            return;
        }
        else if (GameManager.instance.pause == false)
        {
            currentAttackTime2 = currentAttackTime2 + Time.deltaTime;
            Enemy2.isStopped = false;

            if (canAttack == false)
            {
                print("moving");
                //canMove = true;
                //audioSource.PlayOneShot(walkSound);
                Enemy2.SetDestination(player.position);

            }

        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            audioSource.PlayOneShot(damageSound);
            enemy2Health--;
            Destroy(collision.gameObject);

            if (enemy2Health <= 0)
            {
                audioSource.PlayOneShot(explosionSound);
                StartCoroutine(dyingSecond());
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {

            canAttack = true;

            if (currentAttackTime2 >= TimeBetweenAttacks)
            {
                currentAttackTime2 = 0;
                GameManager.instance.MinusHealth(DamageRate);
                audioSource.PlayOneShot(shootSound);

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
