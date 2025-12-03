using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathing : MonoBehaviour
{
    public NavMeshAgent navMesh;

    public Transform player;
    public Transform PointA;
    public Transform PointB;

    public LayerMask isGround, playerlocation;
    public Vector3 walkPoint;
    bool destination;
    public float walkRange;

    //__Attacking vals__
    public float attackCooldown;
    bool attacking;
    
    //__States___
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackRange;

    // Patrol helper
    private Transform currentTarget;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        navMesh = GetComponent<NavMeshAgent>();

        // Start patrol at PointA
        currentTarget = PointA;
        navMesh.SetDestination(currentTarget.position);
    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerlocation);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerlocation);

        if (!playerInSight && !playerInAttackRange) Patrolling();
        if (playerInSight && !playerInAttackRange) ChasePlayer();
        if (playerInSight && playerInAttackRange) AttackingPlayer();
    }

    private void Patrolling()
    {
        // If close to current target, switch to the other point
        if (!navMesh.pathPending && navMesh.remainingDistance < 0.5f)
        {
            currentTarget = (currentTarget == PointA) ? PointB : PointA;
            navMesh.SetDestination(currentTarget.position);
        }
    }

    private void ChasePlayer()
    {
        navMesh.SetDestination(player.position);
    }

    private void AttackingPlayer()
    {
        // Stop moving when attacking
        navMesh.SetDestination(transform.position);

        if (!attacking)
        {
            attacking = true;
            // TODO: Insert attack logic here (damage, animation, etc.)
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        attacking = false;
    }
}
