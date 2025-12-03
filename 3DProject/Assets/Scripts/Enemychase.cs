using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemychase : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float detectionRange = 5f;

    private NavMeshAgent agent;
    private Transform player;
    private Transform target;
    private bool chasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = pointB;
        agent.SetDestination(target.position);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // Chase player
            chasing = true;
            agent.SetDestination(player.position);
        }
        else
        {
            // Resume patrol
            if (chasing)
            {
                chasing = false;
                agent.SetDestination(target.position);
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                target = target == pointA ? pointB : pointA;
                agent.SetDestination(target.position);
            }
        }
    }
}