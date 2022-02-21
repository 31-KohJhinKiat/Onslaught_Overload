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
        Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
       

        transform.position += transform.forward * BulletSpeed * Time.deltaTime;
        timeCreated += Time.deltaTime;
        if (timeCreated >= 10)
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
