using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    private bool real_dead = false;
    private NavMeshAgent Agent;


    public Animator Animator;
    public GameObject Reward;
    public float HP = 100;
    public float MaxHP = 100;
    public GameObject Body;

    public EnemyHealthScript HealthScript;


    public bool IsAttacking
    {
        get { return Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"); }
    }

    public bool IsDead
    {
        get
        {
            return HP <= 0 && Animator.GetCurrentAnimatorStateInfo(0).IsName("Dying") && real_dead;
        }
      
    }

    void Start ()
    {
        Agent = GetComponent <NavMeshAgent>();
    }
	
	void Update ()
	{
	    if (IsDead)
        {
            Destroy(this);
            Body.transform.parent = null;
	        gameObject.SetActive(false);
	    }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet")
        {
            if (Reward != null)
            {
                GameObject g = (GameObject)Instantiate(Reward, transform.position, transform.rotation);
                g.transform.parent = transform.parent;
            }
            other.gameObject.SetActive(false);

            if (HP <= 0)
                real_dead = true;
            else
                HP -= 10;
            if (HP <= 0 && !Animator.GetCurrentAnimatorStateInfo(0).IsName("Dying") && !real_dead) 
            {
                HP = 0;
                Animator.SetTrigger("DoDying");
            }
        }
    }

    
}
