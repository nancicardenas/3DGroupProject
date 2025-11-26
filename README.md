# 3DGroupProject

# Game Name:
Survival in Wilderness 

# Names of group members: 
Rita Yousif, Nanci Cardenas, Ryan Dong, Aiden Suarez

# Description of current target for the game: 
In this game, the character traverses through a dangerous forest, collecting coins to unlock the next level checkpoint while fending off attacks from various wildlife. The character will have a limited max three hearts that can be refilled after gaining a certain amount of coins. The character will start with no weapons, but will gain access to different types of weapons from buying them with their coins. The landscape features are trees, cliffs, quicksand, boulders, etc. The character movement will include jumping, striking, throwing, ducking, etc. 

The game has a terrain that is implemented where the main player would tarverse through in the forwards direction and the enemies(animals) would be coming towards the main player. Once the animal passes the player, it does not cycle back where another one would come instead. For example, if a tiger occurs and passes after the player, then another animal would show such as a rabbit. The flow of the game would basically be spawing at the beginning, avoid enemies, and collect coins which would help increasing lives since lives are limited to three, which means there are levels to the game. Lives would be represented as hearts, which the players would have three of them at the beginning and would lose them when the player gets hit by the enemy. If the player has less than 3 hearts, then after gaining 50 points, then they get to have enough to buy a heart or an additional life. 

In the future, there would be animations that would happen when the player gets to jump, strike, throw, duck or dodge the enemy. There would also be animations regarding the objects in the game. Also, there would also be audio assets that would implemented in the game for the animals in which the animals would be the enemies and each of them would have a sound according to it as it would sound in real life. 


# General goals for each person: 
Nanci C. - Coding/ Level Design
Aiden S. - Coding/ Level Design
Rita Y. - Art/ Animation
Ryan D. - Art/ Animation

# Example of current gameplay:
(video/ gif example of gameplay)
<div>
    <a href="https://www.loom.com/share/a80e2aba8fa1475980fb766a962e68c7">
      <p>CS583 - 3D Game Demo - Watch Video</p>
    </a>
    <a href="https://www.loom.com/share/a80e2aba8fa1475980fb766a962e68c7">
      <img style="max-width:300px;" src="https://cdn.loom.com/sessions/thumbnails/a80e2aba8fa1475980fb766a962e68c7-18b61aee2a2a10d3-full-play.gif#t=0.1">
    </a>
  </div>

# Code:
Enemy movement
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




