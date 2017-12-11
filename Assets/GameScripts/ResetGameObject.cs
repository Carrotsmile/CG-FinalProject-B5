using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameObject : MonoBehaviour {

    public bool reset;
    private float pos_x, pos_y, pos_z, rot_x, rot_y, rot_z, rot_w;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {

        rb = gameObject.GetComponent<Rigidbody>();
        
        pos_x = transform.position.x;
        pos_y = transform.position.y;
        pos_z = transform.position.z;

        rot_x = transform.rotation.x;
        rot_y = transform.rotation.y;
        rot_z = transform.rotation.z;
        rot_w = transform.rotation.w;
    }

    void Update()
    {
        if(reset)
        {
            RestoreToStartState();
            reset = false;
        }
    }

    public void RestoreToStartState()
    {
        if(rb != null) rb.velocity = Vector3.zero;
        transform.position = new Vector3(pos_x, pos_y, pos_z);
        transform.rotation = new Quaternion(rot_x, rot_y, rot_z, rot_w);
    }
}
