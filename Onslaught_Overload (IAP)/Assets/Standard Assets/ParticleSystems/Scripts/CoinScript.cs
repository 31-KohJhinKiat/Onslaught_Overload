using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinScript : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        //collision with coins
        if (collision.gameObject.tag.Equals("Player"))
        {

            GameManager.instance.addScore();
            Destroy(this.gameObject);
            print("score");
        }
    }
}
