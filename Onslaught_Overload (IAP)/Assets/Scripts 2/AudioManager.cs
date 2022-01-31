using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioBgm;
    public AudioClip gameBgm;
    public AudioClip pointSfx;
    public AudioClip contactedSfx;
    public AudioClip uncontactedSfx;
    public AudioClip gameWinSfx;
    public AudioClip gameLoseSfx;
    public AudioClip healthSfx;
    public AudioClip DamageSfx;
    public AudioClip shootSfx;
    public AudioClip reloadSfx;
    public AudioClip damageSfx;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        audioBgm.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBGM()
    {
        audioBgm.Play();
    }

    public void PlayContactedSfx()
    {
        audioSource.PlayOneShot(contactedSfx);
    }

    public void PlayUnContactedSfx()
    {
        audioSource.PlayOneShot(uncontactedSfx);
    }

    public void PlayShootSfx()
    {
        audioSource.PlayOneShot(shootSfx);
    }

    public void PlayReloadSfx()
    {
        audioSource.PlayOneShot(reloadSfx);
    }

    public void PlayDamageSFX()
    {
        audioSource.PlayOneShot(damageSfx);
    }
}
