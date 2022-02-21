using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject Enemy1;
    public GameObject Enemy2;

    public float spawnInterval;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndSpawn(spawnInterval));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 1.05f, Random.Range(minZ, maxZ));

            //Instantiate(Enemy1, spawnPosition, Quaternion.identity);

            int RandomNumber = Random.Range(0, 2);

            if (RandomNumber == 0)
            {
                Instantiate(Enemy1, spawnPosition, Quaternion.identity);
            }

            else
            {
                Instantiate(Enemy2, spawnPosition, Quaternion.identity);
            }
        }
    }

}
