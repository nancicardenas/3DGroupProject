using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPTP : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Transform target;

    void Start()
    {
        target = pointB; // start moving toward point B
    }

    void Update()
    {
        // Move toward the target
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // If reached target, switch to the other point
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = target == pointA ? pointB : pointA;
        }

        // Optional: make enemy face the direction it's moving
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }
    }
}
