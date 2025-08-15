using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    [System.Serializable]
    public class Order
    {
        // total number of burgers and fries needed for the order and the time limit for the order
        public int burgersNeeded; 
        public int friesNeeded;
        public float timeLimit;
    }

    // list of daily orders representing a day each
    public List<Order> dailyOrders = new List<Order>();

    public UIManager UIManager; 

    private int currentDay = 0;
    private int burgersCount = 0;
    private int friesCount = 0;
    private float timer = 0f;
    private bool orderActive = false;

    // Public properties so that the UIManager can access the order system data and display it
    public bool OrderActive => orderActive;
    public int DayNumber => currentDay + 1;
    public int BurgersRemaining => Mathf.Max(0, dailyOrders[currentDay].burgersNeeded - burgersCount);
    public int FriesRemaining => Mathf.Max(0, dailyOrders[currentDay].friesNeeded - friesCount);
    public float TimeRemaining => Mathf.Max(0f, dailyOrders[currentDay].timeLimit - timer);

    void Start()
    {
        StartDay(0);
    }

    void Update()
    {

        if (!orderActive) return;

        timer += Time.deltaTime;

        // If the timer exceeds the current order's time limit, the player loses and the lose screen is shown
        // by calling the ShowLoseScreen function from the UIManager.
        if (timer >= dailyOrders[currentDay].timeLimit)
        {
            orderActive = false;
            UIManager.ShowLoseScreen();
        }
    }

    void StartDay(int dayIndex)
    {
        // If there are no more orders to complete , show the win screen.
        // by calling the ShowWinScreen function from the UIManager.
        if (dayIndex >= dailyOrders.Count)
        {
            orderActive = false;
            UIManager.ShowWinScreen();
            return;
        }

        // Reset values for the new day.
        currentDay = dayIndex;
        burgersCount = 0;
        friesCount = 0;
        timer = 0f;
        orderActive = true;
    }

    void NextDay()
    {
        StartDay(currentDay + 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        // check if the collided object is a burger and we still need burgers,
        // if so,then increment the burgersCount and destroy the burger object.

        if (collision.gameObject.CompareTag("Burger") && BurgersRemaining > 0)
        {
            burgersCount++;
            Destroy(collision.gameObject);
        }
        // same thing with burgers but for fries.
        else if (collision.gameObject.CompareTag("Fries") && FriesRemaining > 0)
        {
            friesCount++;
            Destroy(collision.gameObject);
        }

        // If both burgers and fries are completed for the current order then we call the NextDay function
        // to start the next day.
        if (BurgersRemaining == 0 && FriesRemaining == 0)
        {
            NextDay();
        }
    }
}