using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public string requiredTag; 
    public GameObject outputPrefab; 
    public float requiredTime = 0f; 
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

                TransformObject();
            }
            else
            {

                isInTrigger = true;
                timer = 0f;
            }
        }
    }
}