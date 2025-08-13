using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergingManager : MonoBehaviour
{
    public string ingredientATag = "Meat";
    public string ingredientBTag = "Bread";
    public GameObject resultPrefab;

    private List<GameObject> ingredientsOnPlate = new List<GameObject>();

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag(ingredientATag) || other.CompareTag(ingredientBTag))
        {
            if (!ingredientsOnPlate.Contains(other))
                ingredientsOnPlate.Add(other);

            Merge();
        }
    }
    void Merge()
    {
        GameObject meat = ingredientsOnPlate.Find(obj => obj.CompareTag(ingredientATag));
        GameObject bread = ingredientsOnPlate.Find(obj => obj.CompareTag(ingredientBTag));

        if (meat != null && bread != null)
        {
            Vector3 spawnPos = (meat.transform.position + bread.transform.position) / 2f;
            Instantiate(resultPrefab, spawnPos, Quaternion.identity);

            Destroy(meat);
            Destroy(bread);

            ingredientsOnPlate.Remove(meat);
            ingredientsOnPlate.Remove(bread);
        }
    }
}