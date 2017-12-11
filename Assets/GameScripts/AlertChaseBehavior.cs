using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertChaseBehavior : MonoBehaviour {

    public float step;
    public bool isChase;
    public bool hasCaptured;

    private bool isCapturing;
    private LineOfSightTest los;
    private float blindTime;
    private NavMeshAgent navMeshAgent;
    private UnitySteeringController usc;
    private float captureTime;
    private float timeCapturing;
    
	// Use this for initialization
	void Start () {
        los = gameObject.GetComponent<LineOfSightTest>();
        isChase = false;
        blindTime = 4.0f;
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        usc = gameObject.GetComponent<UnitySteeringController>();
        captureTime = 1.0f;
        timeCapturing = 0.0f;
        if(step < 0.01f)
        {
            step = 1.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(los.hasLineOfSight && !isChase)
        {
            Vector3 dir = los.player.transform.position - transform.position;
            dir.y = 0.0f; //to prevent very strange rotation , this -> no y rotation
            Vector3 newFace = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newFace);
            if(los.SeenTheft())
            {
                isChase = true;
                navMeshAgent.ResetPath();
                usc.maxSpeed = 7.5f;
            }
        }
        if(isChase && los.hasLineOfSight)
        {
            blindTime = 4.0f;
        }
        if(isChase && !los.hasLineOfSight)
        {
            blindTime -= Time.deltaTime;
            if(blindTime < 0.0f)
            {
                isChase = false;
                //navMeshAgent.isStopped = true;
                usc.maxSpeed = 2.2f;
            }
        }
        if(isChase)
        {
            navMeshAgent.SetDestination(los.player.transform.position);
        }

        //this is when the pursuing npc is within a certain distance of the player 
        if(isCapturing)
        {
            timeCapturing += Time.deltaTime;
            if(timeCapturing > captureTime)
            {
                hasCaptured = true;
                los.player.SendMessage("Capture");
                isChase = false;
                isCapturing = false;
            }
        }

        //when npc gets too close to player, it initiates capture time
        float dist = (gameObject.transform.position - los.player.transform.position).magnitude;
        if(isChase)
        {
            Debug.Log(dist);
        }
        if(dist < 1.5f && isChase)
        {
            isCapturing = true;
        }
	}
}
