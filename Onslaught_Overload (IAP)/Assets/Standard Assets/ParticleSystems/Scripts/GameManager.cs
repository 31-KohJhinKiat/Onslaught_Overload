using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private float scoreValue;
    private float totalCoins;
    public float timeLeft;
    public int timeRemining;
    
    public Text ScoreText;
    public Text TimerText;

    private float TimerValue;

    private AudioSource audioSource;
    public AudioClip collectSound;

    public bool pause;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        totalCoins = 6;

        audioSource = GetComponent<AudioSource>();

        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            pause = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (pause == true)
        {

            panel.SetActive(true);

        }
        else
        {
            panel.SetActive(false);
            timeLeft -= Time.deltaTime;
            timeRemining = Mathf.FloorToInt(timeLeft % 60);
            TimerText.text = "Timer: " + timeRemining.ToString();
        }

        if(scoreValue == totalCoins)
        {          
          SceneManager.LoadScene("WinScene");
            
        }

        else if (timeLeft <= 0)
        {
            SceneManager.LoadScene("LoseScene");

        }

    }

    public void addScore()
    {
        audioSource.PlayOneShot(collectSound);
        scoreValue++;
        ScoreText.GetComponent<Text>().text = "Score: " + scoreValue;
        
    }

    public void ResumeButton()
    {
        pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    }
