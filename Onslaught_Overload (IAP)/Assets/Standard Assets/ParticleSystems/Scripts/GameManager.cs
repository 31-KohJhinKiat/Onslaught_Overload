using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    //score
    private float scoreValue;
    public Text ScoreText;

    //ammo
    public Text AmmoText;
    public float AmmoCount;
    public float MaxAmmo;

    //timer
    private float TimerValue;
    public float levelTime;
    private float elapsedGameTime;
    public Text TimerText;

    //audio
    private AudioSource audioSource;
    public AudioClip collectSound;

    //pause
    public bool pause;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

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

            if (levelTime > 0)
            {
                levelTime -= Time.deltaTime;
                TimerText.GetComponent<Text>().text =
                    "Time left: " + FormatTime(levelTime);
            }
            else
            {
                levelTime = 0;
                print("Times up!");

            }


        }



    }

    public void SetTimeText(float time)
    {
        TimerText.text = "Timer: " + FormatTime(time);
    }

    private string FormatTime(float time)
    {
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timerText =
            System.String.Format("{0:00}:{1:00}:{2:000}",
            minutes, seconds, fraction);
        return timerText;
    }

    public void addScore()
    {
        audioSource.PlayOneShot(collectSound);
        scoreValue++;
        ScoreText.GetComponent<Text>().text = "Score: " + scoreValue;
        
    }

    public void AmmoDecrease()
    {
        AmmoText.GetComponent<Text>().text = "Ammo: " + AmmoCount;
        AmmoCount--;
    }

    public void ResumeButton()
    {
        pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
