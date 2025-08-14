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
    public UIManager UIManager; 
    public AudioManager AudioManager;

    private int currentDay = 0;
    private int burgersCount = 0;
    private int friesCount = 0;
    private float timer = 0f;
    private bool orderActive = false;

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

        if (timer >= dailyOrders[currentDay].timeLimit)
        {
            orderActive = false;
            UIManager.ShowLoseScreen();
            AudioManager.PlayLose();
        }
    }

    void StartDay(int dayIndex)
    {
        if (dayIndex >= dailyOrders.Count)
        {
            orderActive = false;
            UIManager.ShowWinScreen();
            AudioManager.PlayWin();
            return;
        }

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
        if (!orderActive) return;

        if (collision.gameObject.CompareTag("Burger") && BurgersRemaining > 0)
        {
            burgersCount++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Fries") && FriesRemaining > 0)
        {
            friesCount++;
            Destroy(collision.gameObject);
        }

        if (BurgersRemaining == 0 && FriesRemaining == 0)
        {
            NextDay();
        }
    }
}