using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestScript : MonoBehaviour
{
    public GameObject Destination;
    public NavMeshAgent Agent;

	// Use this for initialization
	void Start ()
	{
	    Agent.destination = Destination.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
        //Agent.destination = Destination.transform.position;
    }
}
