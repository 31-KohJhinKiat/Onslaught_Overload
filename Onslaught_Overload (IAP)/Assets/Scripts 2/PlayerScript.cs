using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //player camera
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 4f;
    [SerializeField] bool lockCrusor = true;
    CharacterController controller = null;
    float cameraPitch = 0.0f;
    

    //movement
    [SerializeField] float walkSpeed = 6.0f;
    //public float walkSpeed;
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
        controller = GetComponent<CharacterController>();
        if(lockCrusor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

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
            
            return;
        }

        if (IsDead == true)
        {
            return;
        }

        UpdateMouseLook();
        UpdateMovement();   
       
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, - 90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();
        Vector3 velocity = (transform.forward * inputDir.y + transform.right * inputDir.x) * walkSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
