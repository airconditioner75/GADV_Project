using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 100f;
    public Transform cameraTransform;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        // Find the inputs of the mouse if they are moving it 
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // To prevent the player from looking too far up and down


        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // When they try to look up and down, the camera rotates instead of the whole capsule
        transform.Rotate(Vector3.up * mouseX); // This rotates the capsule around, and since the camera is parented to the capsule, it will move it around also
    }

    void HandleMovement()
    {

        // Find the inputs of the movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity 
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
