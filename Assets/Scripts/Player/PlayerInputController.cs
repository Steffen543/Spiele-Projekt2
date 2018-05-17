using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerInputController : MonoBehaviour {

    public NavMeshAgent Agent;
    public GameObject particle;
    public PlayerScript PlayerScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            int lm = LayerMask.GetMask("Default");
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(clickRay, out hit, float.PositiveInfinity, lm))
            {
                Object.Instantiate(particle, hit.point, new Quaternion());


                if (hit.transform.tag == "Enemy")
                {
                    Agent.stoppingDistance = PlayerScript.ShootRadius;
                    PlayerScript.FollowEnemy = hit.transform.GetComponent<EnemyScript>();
                }
                else
                {
                    PlayerScript.FollowEnemy = null;
                    Agent.stoppingDistance = 0f;
                    Agent.SetDestination(hit.point);
                }

               
            }

        }
	}
}
