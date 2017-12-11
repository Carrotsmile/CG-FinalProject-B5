using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionController : MonoBehaviour {

    public Toggle feedMe;
    public Toggle buyMeMedicine;
    public Toggle giveMeMedicine;
    public Toggle feedSister;
    public Toggle buySisterMedicine;
    public Toggle giveSisterMedicine;

    public void CleanUpState()
    {
        feedMe.isOn = false;
        buyMeMedicine.isOn = false;
        giveMeMedicine.isOn = false;
        feedSister.isOn = false;
        buySisterMedicine.isOn = false;
        giveSisterMedicine.isOn = false;
    }
}
