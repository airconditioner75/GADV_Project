using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        //lock the mouse in the center of the screen and hide it so it becomes first person
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        // find the inputs of the mouse if they are moving it 

        // get the horizontal mouse movement (turning left/right)  by sensitivity & frame time.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        // get the vertical mouse movement (looking up/down)  by sensitivity & frame time.
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // To prevent the player from looking too far up and down


        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // When they try to look up and down, the camera rotates instead of the whole capsule
        transform.Rotate(Vector3.up * mouseX); // This rotates the capsule around, and since the camera is parented to the capsule, it will move it around also
    }

    void HandleMovement()
    {

        // find the vertical and horizontal inputs of the movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // convert input into world movement direction relative to where the player is facing.
        // move the charactercontroller in the calculated direction at moveSpeed by deltatime.
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity, take the current velocity and add gravity to it, then move the character controller by the velocity.
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
