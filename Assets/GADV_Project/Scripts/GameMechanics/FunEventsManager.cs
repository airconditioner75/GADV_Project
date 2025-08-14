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
        if (Random.value > eventChance) return;

        if (Random.Range(0, 2) == 0)
            StartCoroutine(TornadoRoutine());
        else
            StartCoroutine(InverseGravityRoutine());
    }

    IEnumerator TornadoRoutine()
    {
        eventRunning = true;

        uiManager.ShowTornadoScreen(screenDuration);

        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            if (!rb || rb.isKinematic || rb.CompareTag(playerTag)) continue;

            Vector3 dir = (Random.insideUnitSphere + Vector3.up * 0.5f).normalized;
            rb.AddForce(dir * tornadoStrength, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(tornadoDuration);
        eventRunning = false;
    }

    IEnumerator InverseGravityRoutine()
    {
        eventRunning = true;

        uiManager.ShowInverseGravityScreen(screenDuration);

        Vector3 inverseAccel = -2f * Physics.gravity;

        float t = 0f;
        while (t < gravityDuration)
        {
            foreach (var rb in FindObjectsOfType<Rigidbody>())
            {
                if (!rb || rb.isKinematic || rb.CompareTag(playerTag)) continue;
                rb.AddForce(inverseAccel, ForceMode.Acceleration);
            }

            t += Time.deltaTime;
            yield return null;
        }

        eventRunning = false;
    }
}