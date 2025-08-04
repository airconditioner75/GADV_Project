using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public DifficultySO difficulty;

    void Start()
    {
        // Access the number of burgers from the DifficultySO scriptable object
        //Debug.Log("Number of Burgers: " + difficulty.numberOfBurgers);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Change the number of burgers when the space key is pressed
        //    difficulty.numberOfBurgers += 1;
        //    Debug.Log("Updated Number of Burgers: " + difficulty.numberOfBurgers);
        //}

        //difficulty.UpdateDifficulty(Time.deltaTime);
    }
}
