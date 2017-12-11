using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightTest : MonoBehaviour {

    public GameObject player;
    public GameObject obsHead;
    public bool hasLineOfSight;
    public bool seesTheft;
    private float fieldOfView;
    private WillControl playerController;
	// Use this for initialization
	void Start () {
        fieldOfView = 75.0f;
        playerController = player.GetComponent<WillControl>();
	}
	
	// Update is called once per frame
	void Update () {
        float ang = Vector3.Angle(player.transform.position - transform.position, transform.forward);
        RaycastHit hit;
        Vector3 direction = player.transform.position - obsHead.transform.position;
        bool anythingHit = Physics.Raycast(obsHead.transform.position, direction, out hit, 100.0f);
        float dist = direction.magnitude;
        float visibilityMod = playerController.isVisible ? 15.0f : 7.5f;
        if (ang <= fieldOfView && anythingHit && hit.collider.tag.Equals("Player") && !playerController.isSneaking)
        {
            Debug.Log("normal sight in daytime");
            hasLineOfSight = true;
        }
        else if(dist <= 10.0f && !playerController.isSneaking && playerController.vel > 1.0f)
        {
            hasLineOfSight = true;
        }
        else if(dist <= visibilityMod && ang <= fieldOfView && anythingHit && hit.collider.tag.Equals("Player"))
        {
            hasLineOfSight = true;
        }
        else
        {
            hasLineOfSight = false;
        }
	}

    public bool SeenTheft()
    {
        return playerController.didSteal;
    }

    
}
