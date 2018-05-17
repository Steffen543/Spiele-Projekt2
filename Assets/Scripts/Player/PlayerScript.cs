using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    private float damageTimer = 0f;
    private NavMeshAgent Agent;
    private PlayerShootScript ShootScript;

    public float HP = 100;
    public float MaxHP = 100;
    public float ShootRadius = 16f;
    public float DamageTime = 2f;

    public Animator Animator;
    public EnemyScript FollowEnemy;

    public bool IsShooting
    {
        get
        {
            return Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        }
    }

    // Use this for initialization
    void Start ()
    {
        Agent = GetComponent<NavMeshAgent>();
        ShootScript = GetComponent<PlayerShootScript>();
    }

    void OnGUI()
    {
        if (FollowEnemy != null)
        {
            FollowEnemy.HealthScript.IsFollowed();
        }

    }
	
	// Update is called once per frame
	void Update ()
	{
        Agent.isStopped = IsShooting;
        if (FollowEnemy != null)
        {
            if (FollowEnemy.IsDead)
                FollowEnemy = null;
            else
                Agent.SetDestination(FollowEnemy.transform.position);
        }
        CheckFollowEnemyAttack();

	    damageTimer += Time.deltaTime;

	   
	}

    void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "CollectableItem":
                CollectableItem item = other.gameObject.GetComponent<CollectableItem>();
                switch (item.Item)
                {
                    case "Medikit":
                        HP += 50;
                        if (HP > MaxHP) HP = MaxHP;
                        break;
                }
                other.gameObject.SetActive(false);
                break;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyScript>().IsAttacking && damageTimer >= DamageTime)
            {
                HP -= 10;
                damageTimer = 0f;
            }
        }
    }

    void CheckFollowEnemyAttack()
    {
        if (FollowEnemy != null)
        {
            var away = Vector3.Distance(transform.position, FollowEnemy.transform.position);
            if (away <= ShootRadius)
            {
                Vector3 direction = (FollowEnemy.transform.position - transform.position).normalized;
                //direction.z -= 0.9f;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
                Animator.SetTrigger("DoShoot");


                //transform.LookAt(FollowEnemy.transform);
                Attack(FollowEnemy);
            }
        }
    }

    void Attack(EnemyScript enemy)
    {
        ShootScript.Shoot(enemy.transform.position);
    }

}
