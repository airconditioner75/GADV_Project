using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public string requiredTag; // Tool or station tag e.g. "Knife" or "Fryer"
    public GameObject outputPrefab; // The object to spawn after transformation
    public float requiredTime = 0f; // 0 means instantly transform to the next object, this is for cutting/kneading
    private float timer = 0f;
    private bool isInTrigger = false;

    private void Update()
    {
        if (isInTrigger && requiredTime > 0f)
        {
            timer += Time.deltaTime;
            if (timer >= requiredTime)
            {
                TransformObject();
            }
        }
    }

    private void TransformObject()
    {
        Instantiate(outputPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(requiredTag))
        {
            if (requiredTime <= 0f)
            {
                // Instant transformation
                TransformObject();
            }
            else
            {
                // Start timing
                isInTrigger = true;
                timer = 0f;
            }
        }
    }
}