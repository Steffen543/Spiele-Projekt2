using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationController : MonoBehaviour {

    private NavMeshAgent Agent;
    public Animator Animator;
   

    // Use this for initialization
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        var isWalking = Agent.velocity.magnitude > 0.01f;
        Animator.SetBool("IsWalking", isWalking);
     
    }
}
