using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergingManager : MonoBehaviour
{
    public string ingredientATag = "Meat";
    public string ingredientBTag = "Bread";
    public GameObject resultPrefab;

    // List to keep track of ingredients on the plate
    private List<GameObject> ingredientsOnPlate = new List<GameObject>();

    void OnCollisionEnter(Collision collision)
    {
        // Get the object that collided with the plate
        GameObject other = collision.gameObject;

        // Check if the collided object is either meat or bread
        if (other.CompareTag(ingredientATag) || other.CompareTag(ingredientBTag))
        {
            // only add the ingredient to the list if it’s not already inside.
            if (!ingredientsOnPlate.Contains(other))
                ingredientsOnPlate.Add(other);

            Merge();
        }
    }
    void Merge()
    {
        // Look through the ingredient list to find an object with the ingredient a and b tag.
        GameObject meat = ingredientsOnPlate.Find(obj => obj.CompareTag(ingredientATag));
        GameObject bread = ingredientsOnPlate.Find(obj => obj.CompareTag(ingredientBTag));

        // once both ingredients are found,
        if (meat != null && bread != null)
        {
            //then we find the position of the two ingredients and instantiate the result prefab at the midpoint of their positions
            Vector3 spawnPos = (meat.transform.position + bread.transform.position) / 2f;
            Instantiate(resultPrefab, spawnPos, Quaternion.identity);

            //destory the ingredients in the scene and remove them from the list, so it looks like they merged
            Destroy(meat);
            Destroy(bread);

            ingredientsOnPlate.Remove(meat);
            ingredientsOnPlate.Remove(bread);
        }
    }
}