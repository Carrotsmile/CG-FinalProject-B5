using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayMenuController : MonoBehaviour {

    public GameObject HealthComponent;
    public GameObject Inventory;
    public GameObject PlayerActions;
    public GameObject CarriageComponent;
    public GameObject ResultList;
    public GameObject NextDayComponent;

    public GameObject Home;

    public GameObject dayNightObject;

    public GameObject VictoryScreen;

    private HealthController healthController;
    private InventoryController inventoryController;
    private PlayerActionController playerActionController;
    private CarriageComponentController carriageComponentController;
    private ResultListController resultListController;
    private NextDayComponentController nextDayComponentController;

    private PlayerHomeController homeController;

    void Awake()
    {
        healthController = HealthComponent.GetComponent<HealthController>();
        inventoryController = Inventory.GetComponent<InventoryController>();
        playerActionController = PlayerActions.GetComponent<PlayerActionController>();
        carriageComponentController = CarriageComponent.GetComponent<CarriageComponentController>();
        resultListController = ResultList.GetComponent<ResultListController>();
        nextDayComponentController = NextDayComponent.GetComponent<NextDayComponentController>();

        Debug.Log("Day Menu being initialized");
        Debug.Log(nextDayComponentController);

        homeController = Home.GetComponent<PlayerHomeController>();
    }

    // Use this for initialization
    void Start ()
    { 
        //add listeners to the ui elements
        nextDayComponentController.nextButton.onClick.AddListener(progressDay);
        playerActionController.buyMeMedicine.onValueChanged.AddListener((v) =>
       {
           playerActionController.giveMeMedicine.isOn = v;
           playerActionController.giveMeMedicine.interactable = !v;
           if (v)
           {
               inventoryController.money.text = (int.Parse(inventoryController.money.text) - 25).ToString();
               //inventoryController.money.text = (inventoryController.moneyCount - 25).ToString();
               inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) + 1).ToString();
               if (inventoryController.moneyCount < 50)
               {
                   playerActionController.buySisterMedicine.interactable = false;
               }
           }

           else
           {
               inventoryController.money.text = (int.Parse(inventoryController.money.text) + 25).ToString();
               inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) - 1).ToString();
               if (inventoryController.moneyCount >= 25)
               {
                   playerActionController.buySisterMedicine.interactable = true;
               }
           }
       });
        //this listener should make it so that you can't decided to give your sister medicine and buy her medicine seperately
        playerActionController.buySisterMedicine.onValueChanged.AddListener((v) =>
        {
            playerActionController.giveSisterMedicine.isOn = v;
            if (v)
            {
                inventoryController.money.text = (int.Parse(inventoryController.money.text) - 25).ToString();
                inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) + 1).ToString();
                if (inventoryController.moneyCount < 50)
                {
                    playerActionController.buyMeMedicine.interactable = false;
                }
            }

            else
            {
                inventoryController.money.text = (int.Parse(inventoryController.money.text) + 25).ToString();
                inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) - 1).ToString();
                if (inventoryController.moneyCount >= 25)
                {
                    playerActionController.buyMeMedicine.interactable = true;
                }
            }
        });

        //feeding player toggle
        playerActionController.feedMe.onValueChanged.AddListener((v) =>
        {
            if (v)
            {
                //inventoryController.food.text = (inventoryController.foodCount - 5).ToString();
                inventoryController.food.text = (int.Parse(inventoryController.food.text) - 5).ToString();
                if (inventoryController.foodCount < 10)
                {
                    playerActionController.feedSister.interactable = false;
                }
            }
            else
            {
                inventoryController.food.text = (int.Parse(inventoryController.food.text) + 5).ToString();
                if (inventoryController.foodCount >= 5)
                {
                    playerActionController.feedSister.interactable = true;
                }
            }
        });
        
        //feeding sister toggle
        playerActionController.feedSister.onValueChanged.AddListener((v) =>
        {
            if (v)
            {
                inventoryController.food.text = (int.Parse(inventoryController.food.text) - 5).ToString();
                if (inventoryController.foodCount < 10)
                {
                    playerActionController.feedMe.interactable = false;
                }
            }
            else
            {
                inventoryController.food.text = (int.Parse(inventoryController.food.text) + 5).ToString();
                if (inventoryController.foodCount >= 5)
                {
                    playerActionController.feedMe.interactable = true;
                }
            }
        });

        //give me medicine toggle
        playerActionController.giveMeMedicine.onValueChanged.AddListener((v) =>
        {
            if (v)
            {
                //inventoryController.medicine.text = (inventoryController.medicineCount - 1).ToString();
                inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) - 1).ToString();
                if(inventoryController.medicineCount < 2)
                {
                    playerActionController.giveSisterMedicine.interactable = false;
                }
            }
            else
            {
                //inventoryController.medicine.text = (inventoryController.medicineCount).ToString();
                inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) + 1).ToString();
                if (inventoryController.medicineCount >= 1)
                {
                    playerActionController.giveSisterMedicine.interactable = true;
                }
            }
        });
        //give sister medicine toggle
        playerActionController.giveSisterMedicine.onValueChanged.AddListener((v) =>
        {
            if (v)
            {
                inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) - 1).ToString();
                if (inventoryController.medicineCount < 2)
                {
                    playerActionController.giveMeMedicine.interactable = false;
                }
            }
            else
            {
                inventoryController.medicine.text = (int.Parse(inventoryController.medicine.text) + 1).ToString();
                if (inventoryController.medicineCount >= 1)
                {
                    playerActionController.giveMeMedicine.interactable = true;
                }
            }
        });
        carriageComponentController.purchase.onClick.AddListener(ClaimVictory);
    }

    void ClaimVictory()
    {
        VictoryScreen.SetActive(true);
    }

    //this handles: signaling that the menu should be closed, getting player selected options and info
    void progressDay()
    {
        homeController.menuFinished = true;
        IncrementDay();
        dayNightObject.SendMessage("RevertToStart");
    }
	
	public void ChangeCharacterState(int meHealth, string meStatus, int sisterHealth, string sisterStatus)
    {
        healthController.MeHealth.text = meHealth.ToString() + " hp";
        healthController.MeStatus.text = meStatus;
        healthController.SisterHealth.text = sisterHealth.ToString() + " hp";
        healthController.SisterStatus.text = sisterStatus;
    }

    public void ToggleBuyMedicine()
    {

    }
    //should be called upon initializing the player next day ui
    public void ChangeInventoryState(int food, int money, int medicine)
    {
        inventoryController.SetElements(food, money, medicine);
        if(food <= 0)
        {
            playerActionController.feedMe.interactable = false;
            playerActionController.feedSister.interactable = false;
        } else
        {
            playerActionController.feedMe.interactable = true;
            playerActionController.feedSister.interactable = true;
        }

        if (money < 25)
        {
            playerActionController.buyMeMedicine.interactable = false;
            playerActionController.buySisterMedicine.interactable = false;
        }
        else
        {
            playerActionController.buyMeMedicine.interactable = true;
            playerActionController.buySisterMedicine.interactable = true;
        }

        if (medicine <= 0)
        {
            playerActionController.giveMeMedicine.interactable = false;
            playerActionController.giveSisterMedicine.interactable = false;
        }
        else
        {
            playerActionController.giveMeMedicine.interactable = true;
            playerActionController.giveSisterMedicine.interactable = true;
        }

        if (money < 500)
        {
            carriageComponentController.purchase.interactable = false;
        } else
        {
            carriageComponentController.purchase.interactable = true;
        }
    }

    public void PopulateResultList(List<string> results)
    {
        Debug.Log(results.Count);
        Debug.Log(resultListController);
        resultListController.SetResults(results);
    }

    public void IncrementDay()
    {
        nextDayComponentController.IncrementDay();
    }

    public void GetPlayerSelectedData(out bool feedMe, out bool giveMeMedicine, out bool buyMe, out bool feedSister, out bool giveSisterMedicine, out bool buySister)
    {
        feedMe = playerActionController.feedMe.isOn;
        giveMeMedicine = playerActionController.giveMeMedicine.isOn;
        buyMe = playerActionController.buyMeMedicine.isOn;
        feedSister = playerActionController.feedSister.isOn;
        giveSisterMedicine = playerActionController.giveSisterMedicine.isOn;
        buySister = playerActionController.buySisterMedicine.isOn;
    }

    public void CleanUpWidgetState()
    {
        playerActionController.CleanUpState();
    }
}
