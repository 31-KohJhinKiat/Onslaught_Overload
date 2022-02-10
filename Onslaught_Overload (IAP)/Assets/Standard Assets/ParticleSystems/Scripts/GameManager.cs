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

    //win
    public bool Win;
    public GameObject winPanel;

    //lose
    public bool Lose;
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

        pause = false;
        Win = false;
        Lose = false;
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        crosshairs.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Pause();
        }

        if (pause == true)
        {
            crosshairs.SetActive(false);
            pausePanel.SetActive(true);
            

        }
        else
        {
            crosshairs.SetActive(true);
            pausePanel.SetActive(false);

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
                Lose = true;

            }

        }

        if (Lose == true)
        {
            LoseGame();
        }

        if (Win == true)
        {
            WinGame();
        }

    }

    

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
            Lose = true;
            
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

    public void ResumeButton()
    {
        pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoseGame()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshairs.SetActive(false);
        losePanel.SetActive(true);        
    }

    public void WinGame()
    {

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshairs.SetActive(false);
        winPanel.SetActive(true);
       
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
