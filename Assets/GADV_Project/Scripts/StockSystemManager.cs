using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockSystemManager : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public LayerMask interactableLayer;
    public float distanceToPress = 3f;
    public float cooldownDuration = 5f;

    private bool isOnCooldown = false;

    void Update()
    {
        CastRayAndCheckForButtonPress();
    }

    public void CastRayAndCheckForButtonPress()
    {
        if (Input.GetMouseButtonDown(0) && !isOnCooldown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, distanceToPress, interactableLayer))
            {
                StockSystemManager button = hit.transform.GetComponentInParent<StockSystemManager>();
                if (button != null && button == this)
                {
                    button.SpawnObject();
                    StartCoroutine(CooldownRoutine());
                }
            }
        }
    }

    public void SpawnObject()
    {
        Vector3 position = transform.position + Vector3.left * 1f;
        Instantiate(prefabToSpawn, position, Quaternion.identity);
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}



