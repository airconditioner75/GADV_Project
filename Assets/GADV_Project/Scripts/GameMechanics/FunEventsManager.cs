using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunEvents : MonoBehaviour
{
    public float intervalSeconds = 30f;          
    public float eventChance = 0.5f;

    public float tornadoDuration = 1f;           
    public float tornadoStrength = 10f;          

    public float gravityDuration = 3f;           

    public float screenDuration = 2f;            

    public string playerTag = "Player";          
    public UIManager uiManager;                  

    private float timer = 0f;
    private bool eventRunning = false;

    void Update()
    {
        // check if an event is running, if no then increment the timer, 
        // once the timer reaches the intervalSeconds, reset it and start a random event.
    
        if (eventRunning) return;

        timer += Time.deltaTime;
        if (timer >= intervalSeconds)
        {
            timer = 0f;
            StartRandomEvent();
        }
    }

    void StartRandomEvent()
    {

        // check if a random event should occur based on the eventChance,
        // if it does, randomly choose between a tornado or inverse gravity event.
        // if the result is 0 it starts the TornadoRoutine, but if its  1 it starts the InverseGravityRoutine.
        if (Random.value > eventChance) return;

        if (Random.Range(0, 2) == 0)
            StartCoroutine(TornadoRoutine());
        else
            StartCoroutine(InverseGravityRoutine());
    }

    IEnumerator TornadoRoutine()
    {

        // It sets eventRunning to true, displays the tornado event screen by finding the UIManager and 
        // calling ShowTornadoScreen with the screenDuration.
        eventRunning = true;

        uiManager.ShowTornadoScreen(screenDuration);

        // Loop through all rigidbodies in the scene, checks if they are not kinematic and not the player then ignore and continue
        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            if (!rb || rb.isKinematic || rb.CompareTag(playerTag)) continue;

            //  if not then add a force, the force is calculated by taking a random point inside a unit sphere, adding an upward vector,
            // normalizing it, and multiplying it by the tornadoStrength, then applies a impulse force in a random upward direction
            Vector3 dir = (Random.insideUnitSphere + Vector3.up * 0.5f).normalized;
            rb.AddForce(dir * tornadoStrength, ForceMode.Impulse);
        }

        // then wait for the tornado duration to end, and set eventRunning to false, so that other events can be started.
        yield return new WaitForSeconds(tornadoDuration);
        eventRunning = false;
    }

    IEnumerator InverseGravityRoutine()
    {
        // starting is similiar to the TornadoRoutine
        eventRunning = true;

        uiManager.ShowInverseGravityScreen(screenDuration);

        //calculates inverseAccel as -2 times the normal Physics.gravity vector, making it push objects upward twice as strong
        Vector3 inverseAccel = -2f * Physics.gravity;

        //timer that tells how long the inverse gravity should be
        float t = 0f;
        while (t < gravityDuration)
        {
            // Loop through all rigidbodies in the scene, checks if they are not kinematic and not the player then ignore and continue
            foreach (var rb in FindObjectsOfType<Rigidbody>())
            {
                if (!rb || rb.isKinematic || rb.CompareTag(playerTag)) continue;
                // if not then apply the inverse acceleration to all non-kinematic rigidbodies that are not the player.
                rb.AddForce(inverseAccel, ForceMode.Acceleration);
            }

            // increment the timer each frame until the inverse gravity duration ends
            t += Time.deltaTime;
            yield return null;
        }
        // then wait for the inverse gravity duration to end, and set eventRunning to false, so that other events can be started.
        eventRunning = false;
    }
}