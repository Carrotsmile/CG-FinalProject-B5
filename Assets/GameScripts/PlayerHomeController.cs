using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHomeController : MonoBehaviour {

    //player must be set, this seems most convienent
    public GameObject player;
    public GameObject itemsParent;
    public bool isTriggerable;
    public bool isClose;

    public GameObject NextDayUI;
    public GameObject DisplayNextDayOption;
    public GameObject PlayerCapturedScreen;

    public GameObject NPCUpdater;

    public bool menuTriggered;
    public bool menuFinished;

    private DayMenuController dayMenuController;
    private ObjectSpawnController itemController;
    private GameObject npcController;

    private HouseInventory houseInventory;
    private PlayerInventory playerInventory;

    private WillControl playerController;

    // Use this for initialization
    void Start () {
        itemController = itemsParent.GetComponent<ObjectSpawnController>();
        playerInventory = player.GetComponent<PlayerInventory>();
        playerController = player.GetComponent<WillControl>();
        houseInventory = GetComponent<HouseInventory>();
        dayMenuController = NextDayUI.GetComponent<DayMenuController>();
        menuTriggered = false;
        menuFinished = false;
	}
	
	// Update is called once per frame
	void Update () {
        float d = 0.0f;
        d = (transform.position - player.transform.position).magnitude;
        if(d < 5.0f)
        {
            isClose = true;
            if (!menuTriggered)
            {
                DisplayNextDayOption.SetActive(true);
                DisplayNextDayOption.GetComponent<Text>().text = "PRESS [T] TO GO TO THE NEXT DAY";
            }
        }
        else
        {
            DisplayNextDayOption.SetActive(false);
            isClose = false;
        }
		if(Input.GetKeyDown(KeyCode.T) && isClose && isTriggerable && !menuTriggered)
        {
            menuTriggered = true;
            menuFinished = false;
            NextDayUI.SetActive(true);


            //keeps the names of all the items obtained as they are deleted when redeemed
            dayMenuController.PopulateResultList(playerInventory.items);

            //finds out how much stuff was obtained today and adds it to the house Inventory
            int f, m, med;
            playerInventory.RedeemInventory(out f, out m, out med);
            houseInventory.food += f;
            houseInventory.money += m;
            houseInventory.medicine += med;
            houseInventory.PassDay();

            dayMenuController.ChangeInventoryState(houseInventory.food, houseInventory.money, houseInventory.medicine);
            dayMenuController.ChangeCharacterState(houseInventory.playerHealth, houseInventory.playerHealthStatus + " " + houseInventory.playerHungerStatus,
                                                   houseInventory.sisterHealth, houseInventory.sisterHealthStatus + " " + houseInventory.sisterHungerStatus);
            dayMenuController.CleanUpWidgetState();
        }
        if(menuFinished)
        {
            //find out what the player selected during nextDayMenu screen
            bool meFed, meMed, meBuy, sisFed, sisMed, sisBuy;
            dayMenuController.GetPlayerSelectedData(out meFed, out meMed, out meBuy, out sisFed, out sisMed, out sisBuy);

            int moneySpent = 0;
            moneySpent += (meBuy ? 25 : 0) + (sisBuy ? 25 : 0);

            if (meFed) houseInventory.GiveFood("player");
            if (sisFed) houseInventory.GiveFood("sister");
            if (meMed) houseInventory.GiveMedicine("player");
            if (sisMed) houseInventory.GiveMedicine("sister");

            houseInventory.money -= moneySpent;

            menuTriggered = false;
            menuFinished = false;
            itemController.StartDay();
            NextDayUI.SetActive(false);
        }
        if(playerController.isCaptured)
        {
            Debug.Log("player successfully captured");
            int f, m, med;
            playerInventory.RedeemInventory(out f, out m, out med); //this is all 'wasted' since it was taken away
            PlayerCapturedScreen.SetActive(true);
            NPCUpdater.GetComponent<NPCBehaviorTree>().toReset = true;
            Debug.Log("everything should be reset now");
            houseInventory.PassDay();
            houseInventory.PassDay();
        }
	}
}
