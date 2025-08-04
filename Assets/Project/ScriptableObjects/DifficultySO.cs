using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDifficulty", menuName = "Game/Difficulty")]
public class DifficultySO : ScriptableObject
{
    [Header("Difficulty Settings")]

    [Tooltip("Number of items that appear in this difficulty level.")]
    public int numberOfBurgers;

    // Create an update function that increases the number of burgers every 5 seconds.
    private float updateTimer = 0f;
    public float updateInterval = 5f;
    public void UpdateDifficulty(float deltaTime)
    {
        updateTimer += deltaTime;
        if (updateTimer >= updateInterval)
        {
            numberOfBurgers++;
            updateTimer = 0f; // Reset the timer
            Debug.Log("Difficulty Updated: Number of Burgers = " + numberOfBurgers);
        }
    }

    // Optionally, you can add a method to reset the difficulty settings
}
