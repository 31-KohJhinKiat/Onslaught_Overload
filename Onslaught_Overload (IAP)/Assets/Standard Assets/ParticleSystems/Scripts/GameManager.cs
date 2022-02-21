using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //timer
    private float TimerValue;
    public float levelTime;
    private float elapsedGameTime;
    public Text TimerText;

    //audio
    private AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip healSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    //pause
    public bool pause;
    public GameObject pausePanel;

    //win and lose
    public bool isGameWin;
    public GameObject winPanel;    
    public bool isGameOver;
    public GameObject losePanel;

    //crosshairs
    public GameObject crosshairs;

    //health
    private GameObject Player;
    public GameObject healthBar;
    public float healthCount;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isGameWin = false;
        isGameOver = false;
        pause = false;
        pausePanel.SetActive(false);
        crosshairs.SetActive(true);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (isGameOver)
        {
            return;
        }
            
        //pause
        if (Input.GetKeyDown(KeyCode.P) && pause == false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && pause == true)
        {
            ResumeButton();
        }

        if (pause == true)
        {
            crosshairs.SetActive(false);
            pausePanel.SetActive(true);

            //crusor state
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            crosshairs.SetActive(true);
            pausePanel.SetActive(false);

            //crusor state
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

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
                SetGameOver(false);

            }

        }

    }

    public void Pause()
    {
        pause = true;

        //stop time
        Time.timeScale = 0;
    }

    public void SetGameOver(bool isGameWin)
    {
        //Game over
        isGameOver = true;

        if (!winPanel.activeSelf && !losePanel.activeSelf)
        {
            if (isGameWin)
            {
                

                crosshairs.SetActive(false);
                winPanel.SetActive(true);
                audioSource.PlayOneShot(winSound);
                print("Win");

                Time.timeScale = 0;
            }
            else
            {
                

                crosshairs.SetActive(false);
                losePanel.SetActive(true);
                audioSource.PlayOneShot(loseSound);
                print("Lose");

                Time.timeScale = 0;
            }
        }
    }

    public bool GetIsGameOver()
    {
        //crusor state
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        return isGameOver;
    }

    //Health
    public void UpdateHealthSlider(float health)
    {
        healthBar.GetComponent<Slider>().value = health;
    }

    public void MinusHealth(float minusHealthValue)
    {
        healthCount -= minusHealthValue;
        audioSource.PlayOneShot(damageSound);


        if (healthCount <= 0)
        {
            healthCount = 0;
            SetGameOver(false);

        }

        UpdateHealthSlider(healthCount);
    }

    public void PlusHealth(float plusHealthValue)
    {
        healthCount += plusHealthValue;
        audioSource.PlayOneShot(healSound);


        if (healthCount >= 100)
        {
            healthCount = 100;
        }

        UpdateHealthSlider(healthCount);
    }

    //Buttons
    public void ResumeButton()
    {
        pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
    }

    public void RestartBtn()
    {
        print("restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitBtn()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void nextlvlBtn2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void nextlvlBtn3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void credits()
    {
        SceneManager.LoadScene("CreditScene");
    }

    //Time
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



}
