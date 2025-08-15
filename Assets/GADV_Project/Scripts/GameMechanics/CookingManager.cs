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
            // Increment the timer and once enough time has passed,  insantiaite the output prefab and destory the gameobject
            timer += Time.deltaTime;
            if (timer >= requiredTime)
            {
                InsatantiateObject();
            }
        }
    }

    private void InsatantiateObject()
    {
        // Instantiate the output prefab (eg. cooked meat/ cut potato) at the current position and rotation of this game object
        // and then destroy this game object (eg. raw meat/ potato)
        Instantiate(outputPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the required tag
        if (collision.collider.CompareTag(requiredTag))
        {
            // If cooking time is 0 or less, call the InsatantiateObject
            if (requiredTime <= 0f)
            {

                InsatantiateObject();
            }
            else
            {
                // otherwise, begin timing for the cooking process
                isInTrigger = true;
                timer = 0f;
            }
        }
    }
}