using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FoodName { chicken = 5, cheese = 5, apple = 2, beef = 6, carrot = 3, rice = 4,
                potato  = 5, strawberry = 1, banana = 3, bread = 5};

public class PlayerInventory : MonoBehaviour {

    public List<string> items;
    public GameObject pText;
	// Use this for initialization
	void Start () {
        items = new List<string>();
	}

    public void AddItem(string item)
    {
        items.Add(item);
    }

    public void RedeemInventory(out int food, out int money, out int medicine)
    {
        food = 0;
        money = 0;
        medicine = 0;
        //see if each item falls into which category, food can be of different values
        foreach(string item in items)
        {
            if(!item.Equals("coins") && !item.Equals("medicine"))
            {
                try
                {
                    FoodName fn = (FoodName)Enum.Parse(typeof(FoodName), item);
                    Debug.Log("food redeemed!");
                    Debug.Log((int)fn);
                    food += (int) fn;
                }
                catch (Exception)
                {
                    Debug.Log("object value is not valid:" + item);
                }
            }
            else if(item.Equals("medicine"))
            {
                medicine++;
            }
            else
            {
                if(item.Equals("gold"))
                {
                    money += 50;
                }
                else
                {
                    money += 15;
                }
            }
        }
        //after everything has been counted, empty the inventory
        items.Clear();
    }

}
