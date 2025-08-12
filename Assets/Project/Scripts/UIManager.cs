using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public OrderSystem orderSystem; 
    public TextMeshPro orderText;   

    void Update()
    {
        RefreshOrderText();
    }

    private void RefreshOrderText()
    {
        if (orderSystem == null || orderText == null) return;

        if (!orderSystem.OrderActive)
        {
            orderText.text = "Waiting for next order...";
            return;
        }

        int day = orderSystem.DayNumber;
        int burgersLeft = orderSystem.BurgersRemaining;
        int friesLeft = orderSystem.FriesRemaining;
        int timeLeftSec = Mathf.CeilToInt(orderSystem.TimeRemaining);

        orderText.text =
            "Day: " + day + "\n" +
            "Burgers: " + burgersLeft + "\n" +
            "Fries: " + friesLeft + "\n" +
            "Time: " + timeLeftSec + "s";
    }
}
