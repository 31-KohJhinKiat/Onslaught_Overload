using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float BulletSpeed;
    private float timeCreated;

    // Start is called before the first frame update
    void Start()
    {
        timeCreated = 0;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.pause == true || GameManager.instance.Lose == true || GameManager.instance.Win == true)
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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            Destroy(gameObject);
        }
    }


}
