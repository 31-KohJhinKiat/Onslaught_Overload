using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProtoEnemyScript2 : MonoBehaviour
{
    // navmesh agent
    public NavMeshAgent ProtoCube;

    //Field of view
    public float radius;
    [Range(0, 360)]
    public float angle;
    public GameObject playerRef;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public bool canSeePlayer;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer == true)
        {
            ProtoCube.SetDestination(playerRef.transform.position);
        }
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = 
            Physics.OverlapSphere(transform.position, 
            radius, targetMask);
        

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - 
                transform.position).normalized;


            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = 
                    Vector3.Distance(transform.position, target.position);
                print("seek out 1");

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    print("hunt");
                }
                else
                {
                    canSeePlayer = false;
                    print("seek out 2");
                }
            }
            else
            {
                canSeePlayer = false;

            }

        }
        else if(canSeePlayer)
        {
            canSeePlayer = false;
            print("seek out 4");
        }
    }

}
