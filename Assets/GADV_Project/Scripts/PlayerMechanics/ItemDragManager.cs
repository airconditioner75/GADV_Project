using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragManager : MonoBehaviour
{

    public float maxDistance = 10f; // Reach of the player to drag something
    public LayerMask interactableLayer; // Used layer to make making things interactable easier
    public float dragSpeed = 10f; // How fast the object follows ur mouse            

    private Transform grabbedObject = null;  
    private float grabDistance; // distance between the camera and the grabbed object when picked up.

    void Update()
    {

        //when the left mouse button is pressed(GetMouseButtonDown(0)), TryGrabObject() is called to pick up an object.
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }
        //while the left mouse button is held down (GetMouseButton(0)) , if object can be dragged then make grabbedObject not null, DragObject() moves the object.
        if (Input.GetMouseButton(0) && grabbedObject != null)
        {
            DragObject();
        }

        //when the left mouse button is released (GetMouseButtonUp(0)), DropObject() is called to release the pbject.
        if (Input.GetMouseButtonUp(0) && grabbedObject != null)
        {
            DropObject();
        }

    }

    void TryGrabObject()
    {

        // casts a forward ray starting at the camera's position
        // uses Physics.Raycast to check if the ray hits an object in interactableLayer within maxDistance.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactableLayer))
        {
            // records grabDistance so the object stays the same distance from the camera while held.
            grabbedObject = hit.transform;
            grabDistance = Vector3.Distance(Camera.main.transform.position, grabbedObject.position);

            // If the object has a Rigidbody, then freeze its motion and disabling gravity
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
    { // Get the world position of the camera to use as the ray's origin point, and forward direction of camera so that it casts where player is looking
        Vector3 rayOrigin = Camera.main.transform.position;
        Vector3 rayDirection = Camera.main.transform.forward;

        // Calculate the target position for the grabbed object based on the ray's direction and grab distance
        float offset = 0.03f;
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
            // calculate the movement vector from the current position to the target position.
            // move the Rigidbody smoothly toward the target position based on drag speed and frame time.
            Vector3 moveDirection = targetPosition - grabbedObject.position;
            rb.MovePosition(grabbedObject.position + moveDirection * Time.deltaTime * dragSpeed);
        }
    }

    void DropObject()
    {
        // unfreeze the rigidbody once the player stops holding the object so that physics affects it again
        if (grabbedObject.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.freezeRotation = false;
        }

        grabbedObject = null;
    }

    // this is for my debugging purposes, it draws a red colour ray from the camera to see how far the player can grab something

    //void DrawGrabRay()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    //}
}





