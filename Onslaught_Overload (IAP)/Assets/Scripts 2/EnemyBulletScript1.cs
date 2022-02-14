using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript1 : MonoBehaviour
{
    public float BulletSpeed;
    private float timeCreated;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause == true || GameManager.instance.isGameOver == true)
        {
            return;
        }

        transform.position += transform.forward * BulletSpeed * Time.deltaTime;
        timeCreated += Time.deltaTime;
        if (timeCreated >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            print("hit player");
            GameManager.instance.MinusHealth(5);
            Destroy(this.gameObject);
        }
        //print(other);
        if (other.CompareTag("Wall"))
        {
            print("hit wall");
            Destroy(this.gameObject);
        }

    }

}
