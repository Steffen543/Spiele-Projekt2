using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingEnemyScript : MonoBehaviour
{
    private bool isAggro;
    private float aggroTimer = 0f;
   

    private GameObject target;
    private NavMeshAgent Agent;
    private EnemyScript enemyScript;


    public float FollowRadius = 15f;
    public float AggroTime = 5f;

    void Start()
    {
        target = GameObject.FindWithTag("Player");
        Agent = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<EnemyScript>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isAggro) aggroTimer += Time.deltaTime;
        if (aggroTimer >= AggroTime) isAggro = false;


        if (enemyScript.IsAttacking)
            Agent.isStopped = true;
        else Agent.isStopped = false;


        var away = Vector3.Distance(transform.position, target.transform.position);

        if ((away <= FollowRadius && !enemyScript.IsAttacking) || isAggro)
        {
           
            Agent.destination = target.transform.position;
        }
       
        if (away < 1.5)
        {
            
            enemyScript.Animator.SetTrigger("DoAttack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            isAggro = true;
            Agent.destination = target.transform.position;
        }
    }
}
