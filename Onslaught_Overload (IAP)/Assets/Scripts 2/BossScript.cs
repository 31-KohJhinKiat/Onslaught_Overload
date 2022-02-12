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
    public AudioClip explosionSound;

    //particles

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(StartUpSound);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
