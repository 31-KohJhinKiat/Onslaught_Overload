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
    private bool canAttack;
    public float TimeBetweenAttacks;
    private float currentAttackTime2 = 0.0f;

    //Sounds
    public AudioSource audioSource;
    public AudioClip hoverSound;
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


           print("moving");
           //canMove = true;
           //audioSource.PlayOneShot(walkSound);
           Enemy2.SetDestination(player.position);
           

            if (currentAttackTime2 >= TimeBetweenAttacks)
            {
                currentAttackTime2 = 0;
                Instantiate(enemyBullet, cannon.transform.position,
                cannon.transform.rotation);
                audioSource.PlayOneShot(shootSound);

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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {

            Enemy2.isStopped = true;

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
        //canMove = false;
        Enemy2.isStopped = true;
        ExplosionOn();
        yield return new WaitForSeconds(2.3f);
        Destroy(gameObject);

    }

}
