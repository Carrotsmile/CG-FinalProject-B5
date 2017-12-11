using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseInventory : MonoBehaviour {

    public int money;
    public int food;
    public int medicine;

    public int playerHealth;
    public int sisterHealth;

    public string playerHealthStatus;
    public string playerHungerStatus;

    public string sisterHealthStatus;
    public string sisterHungerStatus;

    public int numberOfDays;

    public bool gameOver;
    public GameObject gameOverScreen;

    // Use this for initialization
    public void Start () {
        gameOver = false;
	}

    // for each day, potentially change status of characters, and set gameover flag if someone runs out of health
    public void PassDay()
    {
        System.Random r = new System.Random();
        int r1 = r.Next(1, 101);
        int r2 = r.Next(1, 101);

        switch (playerHealthStatus)
        {
            case "ok":
                break;
            case "slightly sick":
                break;
            case "very sick":
                playerHealth--;
                break;
            default:
                break;
        }

        switch (sisterHealthStatus)
        {
            case "ok":
                break;
            case "slightly sick":
                break;
            case "very sick":
                sisterHealth--;
                break;
            default:
                break;
        }

        if (playerHungerStatus.Equals("very hungry"))
        {
            playerHealth--;
        }
        if (sisterHungerStatus.Equals("very hungry"))
        {
            sisterHealth--;
        }

        //the potential change in status should be evaluated after characters take potential damage
        if (r1 < 4)
        {
            //very low chance, but main character could also become ill
            if (playerHealthStatus.Equals("ok"))
            {
                playerHealthStatus = "slightly sick";
            }
            else if (playerHealthStatus.Equals("slightly sick"))
            {
                playerHealthStatus = "very sick";
            }
            //cannot become more sick than that
        }

        if (r2 + (numberOfDays * 4) > 80)
        {
            //pretty likely, and increasingly so as time goes on, that the sister character will become more ill
            if (sisterHealthStatus.Equals("ok"))
            {
                sisterHealthStatus = "slightly sick";
            }
            else if (sisterHealthStatus.Equals("slightly sick"))
            {
                sisterHealthStatus = "very sick";
            }
            //cannot become more sick than that
        }

        //final check to see if healths are too low
        if (playerHealth <= 0 || sisterHealth <= 0)
        {
            gameOver = true;
        }

        //keeps track of days that have passed us by
        numberOfDays += 1;

        if(gameOver)
        {
            gameOverScreen.SetActive(true);
        }
    }

    public bool hasNextDay()
    {
        return !gameOver;
    }

    //returns true and decrements medicine if possible, else returns false if not possible
    public bool GiveMedicine(string target)
    {
        if (medicine > 0)
        {
            medicine--;
            if (target.Equals("sister"))
            {
                sisterHealthStatus = "healthy";
            }
            else
            {
                playerHealthStatus = "healthy";
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GiveFood(string target)
    {
        if (food > 0)
        {
            food -= 5;
            if(target.Equals("sister"))
            {
                sisterHungerStatus = "ok";
            } else
            {
                playerHungerStatus = "ok";
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
