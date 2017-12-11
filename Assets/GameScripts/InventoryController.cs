using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

    public Text food;
    public Text money;
    public Text medicine;
    public int foodCount;
    public int moneyCount;
    public int medicineCount;

    public void UpdateElements()
    {
        food.text = foodCount.ToString();
        money.text = moneyCount.ToString();
        medicine.text = medicineCount.ToString();
    }

    public void SetElements(int f, int m, int med)
    {
        foodCount = f;
        moneyCount = m;
        medicineCount = med;
        UpdateElements();
    }
}
