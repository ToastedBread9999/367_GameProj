using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    // Reference to the player's transform
    public Transform playerTransform;

    // Reference to the camera's transform
    public Transform cameraTransform;
    
    // Offset from the camera's position
    public Vector3 offset;

    // Fixed Y position
    public float fixedYoffset;
    // Fixed Y position
    public float fixedYPosition;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player transform is not assigned!");
        }
        
        if (cameraTransform == null)
        {
            Debug.LogError("Camera transform is not assigned!");
        }
        
        // Initialize the fixed Y position based on the current position
        fixedYoffset = playerTransform.position.y;
    }

    void Update()
    {
        // Initialize the fixed Y position based on the current position
        fixedYPosition = playerTransform.position.y + fixedYoffset;

        if (playerTransform != null && cameraTransform != null)
        {
            // Calculate the target position based on the camera's position and rotation
            Vector3 targetPosition = cameraTransform.position + cameraTransform.rotation * offset;

            // Set the position of the object, but keep the Y position fixed
            transform.position = new Vector3(targetPosition.x, fixedYPosition, targetPosition.z);

            // Get the camera's rotation and restrict it to yaw only (horizontal rotation)
            Vector3 eulerRotation = cameraTransform.eulerAngles;
            transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
        }
    }
}