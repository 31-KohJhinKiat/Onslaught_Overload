using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    //health
    public float BossHealth;

    //sounds
    public AudioSource audioSource;
    public AudioClip StartUpSound;
    public AudioClip DamageSound;
    public AudioClip explosionSound;

    //particles
    public ParticleSystem Explosion;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ExplosionOff();
        audioSource.PlayOneShot(StartUpSound);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            audioSource.PlayOneShot(DamageSound);
            BossHealth--;
            Destroy(collision.gameObject);

            if (BossHealth <= 0)
            {
                audioSource.PlayOneShot(explosionSound);
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
        ExplosionOn();
        yield return new WaitForSeconds(2.3f);
        GameManager.instance.SetGameOver(true);

    }

}
