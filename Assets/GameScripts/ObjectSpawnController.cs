using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour {

    public List<GameObject> itemList;
	// Use this for initialization
	void Start () {
        //adds all possible items to the list
        itemList = new List<GameObject>();
        foreach (Transform child in transform)
        {
            foreach(Transform grandchild in child)
            {
                itemList.Add(grandchild.gameObject);
                grandchild.gameObject.SetActive(false);
            }
        }
        System.Random r = new System.Random();
        foreach (GameObject item in itemList.OrderBy(x => r.Next()).Take(15))
        {
            item.SetActive(true);
        }
    }

    void Update()
    {
        
    }

    public void StartDay ()
    {
        foreach (Transform child in transform)
        {
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }
        System.Random r = new System.Random();
        foreach(GameObject item in itemList.OrderBy(x => r.Next()).Take(15))
        {
            item.SetActive(true);
        }
    }
}
