using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public OrderSystem orderSystem;
    public TextMeshPro orderText;
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject tornadoScreen;         
    public GameObject inverseGravityScreen;

    void Start()
    {
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    void Update()
    {
        OrderText();
    }

    void OrderText()
    {
        if (!orderSystem.OrderActive)
        {
            orderText.text = "Waiting for next order...";
            return;
        }

        orderText.text =
            "Day: " + orderSystem.DayNumber + "\n" +
            "Burgers: " + orderSystem.BurgersRemaining + "\n" +
            "Fries: " + orderSystem.FriesRemaining + "\n" +
            "Time: " + Mathf.CeilToInt(orderSystem.TimeRemaining) + "s";
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowTornadoScreen(float seconds)
    {
        if (tornadoScreen == null) return;
        StartCoroutine(ShowPanelForSeconds(tornadoScreen, seconds));
    }

    public void ShowInverseGravityScreen(float seconds)
    {
        if (inverseGravityScreen == null) return;
        StartCoroutine(ShowPanelForSeconds(inverseGravityScreen, seconds));
    }
    IEnumerator ShowPanelForSeconds(GameObject panel, float seconds)
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(seconds);
        panel.SetActive(false);
    }
}



