using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject lvlselectPanel;
    public GameObject plotPanel;
    public GameObject insPanel;

    // Start is called before the first frame update
    void Start()
    {
        lvlselectPanel.SetActive(false);
        plotPanel.SetActive(false);
        insPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lvlselect()
    {
        lvlselectPanel.SetActive(true);
    }

    public void plot()
    {
        plotPanel.SetActive(true);
    }

    public void ins()
    {
        insPanel.SetActive(true);
    }

    public void close1()
    {
        lvlselectPanel.SetActive(false);
    }

    public void close2()
    {
        plotPanel.SetActive(false);
    }

    public void close3()
    {
        insPanel.SetActive(false);
    }

    public void lvl1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void lvl2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void lvl3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void credits()
    {
        SceneManager.LoadScene("CreditScene");
    }

}
