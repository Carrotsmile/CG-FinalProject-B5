using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureTextController : MonoBehaviour {

    private float screenTime;
    private float screenedTime;
	// Use this for initialization
	void Start () {
        screenTime = 5.0f;
        screenedTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        screenedTime += Time.deltaTime;
        if(screenedTime > screenTime)
        {
            gameObject.SetActive(false);
        }
	}

    void OnEnable()
    {
        screenedTime = 0.0f;
    }
}
