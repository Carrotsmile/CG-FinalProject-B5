using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarriageComponentController : MonoBehaviour
{

    public Button purchase;
    public Text remainingAmount;

    private int remaining;

    public void Start()
    {
        remaining = 500;
    }

    public void UpdateRemainingAmount(int money)
    {
        if( money < 500)
        {
            remaining = 500 - money;
        }
        else
        {
            remaining = 0;
        }
        remainingAmount.text = remaining.ToString() + " gp";
    }
}
