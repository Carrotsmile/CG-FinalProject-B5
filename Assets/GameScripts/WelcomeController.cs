using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeController : MonoBehaviour {

    private float ttl;
	// Use this for initialization
	void Start () {
        ttl = 10.0f;
	}
	
	// Update is called once per frame
	void Update () {
        ttl -= Time.deltaTime;
        if(ttl < 0.0f)
        {
            gameObject.SetActive(false);
        }
	}

    void OnEnable()
    {
        ttl = 10.0f;
    }
}
