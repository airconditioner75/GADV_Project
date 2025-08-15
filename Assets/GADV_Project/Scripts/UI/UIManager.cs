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

    void Update()
    {
        OrderText();
    }

    void OrderText()
    {
        // if there is no active order, display filler message
        if (!orderSystem.OrderActive)
        {
            orderText.text = "No more orders!";
            return;
        }

        // display current day number, remaining items, and time left by referencing the orderSystem data
        orderText.text =
            "Day: " + orderSystem.DayNumber + "\n" +
            "Burgers: " + orderSystem.BurgersRemaining + "\n" +
            "Fries: " + orderSystem.FriesRemaining + "\n" +
            "Time: " + Mathf.CeilToInt(orderSystem.TimeRemaining) + "s";
    }

    public void ShowLoseScreen()
    {
        // Show lose screen and pause the game
        // unlock the cursor cause it was in first person, so that the player can click on the restart button
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowWinScreen()
    {
        // Show win screen and pause the game
        // unlock the cursor cause it was in first person, so that the player can click on the restart button
        winScreen.SetActive(true);
        Time.timeScale = 0f; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // Restart the game by reloading the current scene
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator ShowPanelForSeconds(GameObject panel, float seconds)
    {
        // Show the text for tornado and gravity respectively, wait the given time, then hide it again.
        panel.SetActive(true);
        yield return new WaitForSeconds(seconds);
        panel.SetActive(false);
    }
    public void ShowTornadoScreen(float seconds)
    {
        // Show the tornado text for a set number of seconds.
        StartCoroutine(ShowPanelForSeconds(tornadoScreen, seconds));
    }

    public void ShowInverseGravityScreen(float seconds)
    {
        // Show the inverse gravity text for a set number of seconds.
        StartCoroutine(ShowPanelForSeconds(inverseGravityScreen, seconds));
    }
}



