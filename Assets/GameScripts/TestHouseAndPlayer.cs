using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHouseAndPlayer : MonoBehaviour {

    private HouseInventory houseInventory;
    private PlayerInventory playerInventory;

	// Use this for initialization
	void Start () {
        houseInventory = gameObject.GetComponent<HouseInventory>();
        playerInventory = gameObject.GetComponent<PlayerInventory>();
        playerTest();
        houseTest();
	}
	
	// Update is called once per frame
	
    void playerTest()
    {
        Debug.Log("Player inventory test: ");
        int a, b, c;
        playerInventory.RedeemInventory(out a, out b, out c);
        Debug.Log("Empty Player Inventory: " + a.ToString() + ", " + b.ToString() + ", " + c.ToString());
        playerInventory.AddItem("beef");
        playerInventory.AddItem("carrot");
        playerInventory.AddItem("rice");
        playerInventory.AddItem("apple");
        playerInventory.AddItem("cheese");
        playerInventory.AddItem("coin");
        playerInventory.RedeemInventory(out a, out b, out c);
        Debug.Log("Should be: (20, 1, 0) " + a.ToString() + ", " + b.ToString() + ", " + c.ToString());
        playerInventory.RedeemInventory(out a, out b, out c);
        Debug.Log("Empty Player Inventory: " + a.ToString() + ", " + b.ToString() + ", " + c.ToString());
    }
    void houseTest()
    {
        Debug.Log("House Inventory Test");
        printAll();
        for (int i = 0; i < 10; i++)
        {
            houseInventory.PassDay();
            printAll();
        }
    }

    void printAll()
    {
        Debug.Log("House Inventory Information");
        Debug.Log("Money: " + houseInventory.money.ToString());
        Debug.Log("Food: " + houseInventory.food.ToString());
        Debug.Log("Medicine: " + houseInventory.medicine.ToString());

        Debug.Log("Player health: " + houseInventory.playerHealth.ToString());
        Debug.Log("Sister health: " + houseInventory.sisterHealth.ToString());

        Debug.Log("Player Health status: " + houseInventory.playerHealthStatus);
        Debug.Log("sister Health status: " + houseInventory.sisterHealthStatus);

        Debug.Log("Player hunger status: " + houseInventory.playerHungerStatus);
        Debug.Log("sister hunger status: " + houseInventory.sisterHungerStatus);

        Debug.Log("Number of days passed" + houseInventory.numberOfDays.ToString());
        Debug.Log("Is the game over? " + houseInventory.gameOver.ToString());
    }
}
