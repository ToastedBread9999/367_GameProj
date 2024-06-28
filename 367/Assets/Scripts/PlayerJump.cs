using UnityEngine;
using UnityEngine.XR;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = .05f;        // The force applied when jumping
    public float triggerThreshold = 0.8f; // The threshold for the trigger press to initiate jump
    public bool canJump = true;         // Flag to control jumping ability


    void Start()
    {

    }

    void Update()
    {
        // Check if the player wants to jump using the trigger analog value
        if (canJump && IsTriggerPressed())
        {
            Jump();
        }
    }

    bool IsTriggerPressed()
    {
        // Get the active input device (left and right hand controllers)
        InputDevice leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Check if the trigger is pressed on either controller
        bool leftTriggerPressed = leftHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue) && leftTriggerValue > triggerThreshold;
        bool rightTriggerPressed = rightHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue) && rightTriggerValue > triggerThreshold;

        // Return true if either trigger is pressed beyond the threshold
        return leftTriggerPressed || rightTriggerPressed;
    }

    void Jump()
    {
        //Apply jump
        //Vector3 new(Vector3.up * jumpForce, ForceMode.Impulse);
        Vector3 jumpMovement = new Vector3(0, jumpForce*Time.deltaTime, 0);
        transform.position += jumpMovement;
    }
}