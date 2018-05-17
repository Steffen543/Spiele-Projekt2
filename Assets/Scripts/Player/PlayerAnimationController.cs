using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimationController : MonoBehaviour
{

    private float jumpTimer = 0f;

    public NavMeshAgent Agent;
    public Animator Animator;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    jumpTimer += Time.deltaTime;

        var isWalking = Agent.velocity.magnitude > 0.5f;
        Animator.SetBool("IsWalking", isWalking);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animator.SetTrigger("DoShoot");
        }
	    if (Agent.isOnOffMeshLink && jumpTimer > 1.5f)
	    {
            Animator.SetTrigger("DoJump");
	        jumpTimer = 0f;
	    }
    }
}
