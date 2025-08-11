using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragManager : MonoBehaviour
{

    public float maxDistance = 10f; // Reach of the character to drag something
    public LayerMask interactableLayer; // Used layer to make making things interactable easier
    public float dragSpeed = 10f; // How fast the object follows ur mouse            

    private Transform grabbedObject = null;
    private float grabDistance;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        if (Input.GetMouseButton(0) && grabbedObject != null)
        {
            DragObject();
        }

        if (Input.GetMouseButtonUp(0) && grabbedObject != null)
        {
            DropObject();
        }

    }

    void TryGrabObject()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            grabbedObject = hit.transform;
            grabDistance = Vector3.Distance(Camera.main.transform.position, grabbedObject.position);

            if (grabbedObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.freezeRotation = true;
            }
        }
    }


    void DragObject()
    {
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        float offset = 0.01f;
        Vector3 intendedTarget = rayOrigin + rayDirection * grabDistance;
        Vector3 targetPosition = intendedTarget;

        // Check if there's a wall or obstacle between camera and the intended target
        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, grabDistance))
        {
            // Only stop early if the object we hit is not the grabbed object
            if (hit.transform != grabbedObject)
            {
                // Wall or other object is in the way — move object just in front of it
                targetPosition = hit.point - rayDirection * offset;
            }
        }

        if (grabbedObject.TryGetComponent(out Rigidbody rb))
        {
            Vector3 moveDirection = targetPosition - grabbedObject.position;
            rb.MovePosition(grabbedObject.position + moveDirection * Time.deltaTime * dragSpeed);
        }
    }

    void DropObject()
    {
        // Unfreeze the rigidbody once the player stops holding the object so that physics affects it again
        if (grabbedObject.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.freezeRotation = false;
        }

        grabbedObject = null;
    }

    //void DrawGrabRay()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    //}
}





