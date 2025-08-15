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
            // perform a raycast forward from the mouse position up to distanceToPress, only hitting objects in interactableLayer.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, distanceToPress, interactableLayer))
            {
                // try to get a StockSystemManager component 
                StockSystemManager button = hit.transform.GetComponentInParent<StockSystemManager>();
                // ensure the ray hit a specific button before triggering a spawn.
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
        // determine where to spawn the object (1 unit to the left of the button).
        // then we spawn it
        Vector3 position = transform.position + Vector3.left * 1f;
        Instantiate(prefabToSpawn, position, Quaternion.identity);
    }

    private IEnumerator CooldownRoutine()
    {
        // mark the button pressed as being on cooldown, then wait for the cooldown duration before allowing pressing
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }
}



