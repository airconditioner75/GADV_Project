using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDragSystem : MonoBehaviour
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
        // Cast a ray from the camera to where the mouse cursor is, which is locked in the middle
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast to only detect the interactablelayer
        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
        {
            
            grabbedObject = hit.transform;
            grabDistance = Vector3.Distance(transform.position, grabbedObject.position);

            //Freeze the rigidbody of the object so that physics wont affect it while moving around, leaving it active causes bugs
            if (grabbedObject.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.freezeRotation = true;
            }
        }
    }
    void DragObject()
    {
        Vector3 targetPosition = transform.position + transform.forward * grabDistance;

        if (grabbedObject.TryGetComponent(out Rigidbody rb))
        {
            // Move the object using move position so that it colldies with other objects
            Vector3 moveDirection = (targetPosition - grabbedObject.position);
            rb.MovePosition(grabbedObject.position + moveDirection * Time.deltaTime * dragSpeed);
        }
    }

    void DropObject()
    {
        // Unfreeze the rigidbody once the player stops holding the object so that physics affects it again
        if (grabbedObject.TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = true;
            rb.freezeRotation = false;
        }

        grabbedObject = null;
    }
    void DrawGrabRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    }
}





