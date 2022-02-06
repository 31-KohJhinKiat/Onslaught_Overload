using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundScript : MonoBehaviour
{
    public Sprite soundImage;
    public Sprite muteImage;
    public Button SoundButton;
    private bool isOn = true;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        soundImage = SoundButton.image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClicked()
    {
        if (isOn)
        {
            print("activate");
            SoundButton.image.sprite = soundImage;
            isOn = false;
            audioSource.mute = false;
        }
        else
        {
            print("deactivate");
            SoundButton.image.sprite = muteImage;
            isOn = true;
            audioSource.mute = true;
        }
    }
}
