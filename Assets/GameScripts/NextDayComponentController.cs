using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDayComponentController : MonoBehaviour {
    public Text dayCount;
    public Button nextButton;
    private int d;
    public void Start()
    {
        d = 1;
    }
    public void IncrementDay()
    {
        d++;
        dayCount.text = "#" + d.ToString();
    }
}
