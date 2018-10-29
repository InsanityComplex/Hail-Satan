using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : GroupDisable {

    public float Speed;
    public Animator Anim;

    NavMeshAgent agent;
    

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Speed;
    }

    public void Update()
    {
        Vector3 heading = agent.velocity;
        Anim.SetInteger("Horizontal", (int)heading.x);
        Anim.SetInteger("Vertical", (int)heading.z);
    }

    public void Move(Vector3 position)
    {
        agent.SetDestination(position);
    }
}
