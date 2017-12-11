using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    private GameObject player;
    private PlayerInventory pInv;

    public bool isClose;
    public string itemName;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pInv = player.GetComponent<PlayerInventory>();
	}
	
	// Update is called once per frame
	void Update () {

        bool nisClose = (player.transform.position - transform.position).magnitude < 2.5f;
        if(!nisClose && isClose)
        {
            pInv.pText.SetActive(false);
        }
        if(nisClose && !isClose)
        {
            pInv.pText.SetActive(true);
        }
        isClose = nisClose;
        bool keyNeeded = Input.GetKeyDown(KeyCode.E);
        if (isClose && keyNeeded)
        {
            player.SendMessage("AddItem", itemName);
            gameObject.SetActive(false);
            pInv.pText.SetActive(false);
            isClose = false;
        }
	}
}
