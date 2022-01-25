using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy1Script : MonoBehaviour
{
    //damage to player
    public float contactDistance;
    public float damageRate;

    //search for player
    public NavMeshAgent navMeshAgent;
    public GameObject player;
    

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.isStopped = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (GameManager.instance.Pause())
        {
            navMeshAgent.isStopped = true;
            return;
        }*/

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer < contactDistance)
        {
            if (navMeshAgent.isStopped)
            {
                AudioManager.instance.PlayContactedSfx();
                navMeshAgent.isStopped = false;
                animator.SetBool("isMoving", true);
            }

            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            if (!navMeshAgent.isStopped)
            {
                navMeshAgent.isStopped = true;
                AudioManager.instance.PlayUnContactedSfx();
                animator.SetBool("isMoving", false);
            }
        }

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            animator.SetTrigger("isAttacking");
            //GameManager.instance.MinusHealth(damageRate * Time.deltaTime);
        }
    }
}
