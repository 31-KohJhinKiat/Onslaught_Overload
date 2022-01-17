using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //player camera
    Transform playerCamera = null;

    //movement
    public float walkSpeed;
    public float runSpeed;

    //jump
    public Vector3 jump;
    public float jumpForce = 3.0f;
    public bool isGrounded;
    Rigidbody rb;

    //health
    public float playerHealth;

    //gun



    //animation
    public Animator animator;

    //sound
    private AudioSource audioSource;
    public AudioClip walkSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 3.0f, 0.0f);
        //EndText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
