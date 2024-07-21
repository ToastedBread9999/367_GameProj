using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class playerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Movement speed
    public float mouseSensitivity = 100.0f; // Mouse sensitivity for looking around
    public float jumpForce = 5.0f; // Jump force
    public float gravity = -9.81f; // Gravity
    public float fallMultiplier = 2.5f; // Multiplier for faster falling
    public Transform playerBody; // Reference to the player's body
    public Transform vrCamera; // Reference to the VR camera

    private CharacterController characterController;
    private float xRotation = 0f;
    private bool isVRActive = false;
    private Vector3 velocity; // To track velocity for jump and gravity
    private bool isJumping = false; // Track if the player is in the jump state

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        isVRActive = XRSettings.isDeviceActive; // Check if VR device is active
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        if (!isVRActive) // If VR is not active, use mouse look
        {
            LookWithMouse();
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        // Get input from W, A, S, D keys
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply movement
        characterController.Move(move * speed * Time.deltaTime);

        // Apply gravity
        if (!characterController.isGrounded)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
        }
        else if (isJumping)
        {
            velocity.y += gravity * Time.deltaTime; // Gradual fall after jump
        }

        characterController.Move(velocity * Time.deltaTime);

        // Reset velocity when grounded
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // A small negative value to keep the player grounded
            isJumping = false;
        }
    }

    void LookWithMouse()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate vertical rotation and clamp it
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply rotation to the camera (vertical rotation) and player body (horizontal rotation)
        playerBody.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            isJumping = true;
        }
    }

    void LateUpdate()
    {
        if (isVRActive) // If VR is active, align player body with VR camera
        {
            Vector3 vrCameraPosition = vrCamera.position;
            vrCameraPosition.y = transform.position.y; // Keep the player's Y position
            transform.position = vrCameraPosition;

            Vector3 vrCameraRotation = vrCamera.eulerAngles;
            transform.eulerAngles = new Vector3(0, vrCameraRotation.y, 0);
        }
    }
}
