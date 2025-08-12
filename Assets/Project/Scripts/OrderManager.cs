using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSystem : MonoBehaviour
{
    [System.Serializable]
    public class Order
    {
        public int burgersNeeded;
        public int friesNeeded;
        public float timeLimit; 
    }

    public List<Order> dailyOrders = new List<Order>();

    private int currentDay = 0;
    private int burgersCount = 0;
    private int friesCount = 0;
    private float timer = 0f;
    private bool orderActive = false;

    public bool OrderActive => orderActive;
    public int DayNumber => currentDay + 1;
    public int BurgersRemaining => Mathf.Max(0, dailyOrders.Count > 0 ? dailyOrders[currentDay].burgersNeeded - burgersCount : 0);
    public int FriesRemaining => Mathf.Max(0, dailyOrders.Count > 0 ? dailyOrders[currentDay].friesNeeded - friesCount : 0);
    public float TimeRemaining => Mathf.Max(0f, dailyOrders.Count > 0 ? dailyOrders[currentDay].timeLimit - timer : 0f);

    void Start()
    {
        if (dailyOrders == null || dailyOrders.Count == 0)
        {
            return;
        }
        StartDay(0);
    }

    void Update()
    {
        if (!orderActive) return;

        timer += Time.deltaTime;

        if (timer >= dailyOrders[currentDay].timeLimit)
        {
            NextDay();
            return;
        }
    }

    void StartDay(int dayIndex)
    {
        if (dayIndex >= dailyOrders.Count)
        {
            orderActive = false;
            return;
        }

        currentDay = dayIndex;
        burgersCount = 0;
        friesCount = 0;
        timer = 0f;
        orderActive = true;

        var o = dailyOrders[currentDay];
    }

    void NextDay()
    {
        orderActive = false;
        StartDay(currentDay + 1);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!orderActive) return;

        if (collision.gameObject.CompareTag("Burger"))
        {
            if (BurgersRemaining > 0)
            {
                burgersCount++;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Fries"))
        {
            if (FriesRemaining > 0)
            {
                friesCount++;
                Destroy(collision.gameObject);
            }
        }
        else
        {
            return;
        }


        if (BurgersRemaining == 0 && FriesRemaining == 0)
        {
            NextDay();
        }
    }
}