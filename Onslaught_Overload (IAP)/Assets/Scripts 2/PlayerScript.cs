using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private bool IsDead = false;
    public float playerHealth;

    //gun



    //animation
    public Animator animator;

    //sound
    private AudioSource audioSource;
    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioClip landSound;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 3.0f, 0.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause)
        {
            animator.SetBool("walkForward", false);
            animator.SetBool("runForward", false);
            return;
        }

        if (IsDead == true)
        {
            return;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
            animator.SetBool("walkForward", true);

        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * walkSpeed * Time.deltaTime;
            animator.SetBool("walkForward", true);

        }

        /* if (Input.GetKey(KeyCode.W) && (Input.GetKey(KeyCode.LeftShift)))
        {
            transform.position += transform.forward * runSpeed * Time.deltaTime;
            animator.SetBool("runForward", true);

        } */

        if (Input.anyKey == false)
        {
            animator.SetBool("walkForward", false);
        }

        UpdateMouseLook();
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        transform.Rotate(Vector3.up * mouseDelta.x);
    }
}
