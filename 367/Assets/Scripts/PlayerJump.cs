using UnityEngine;
using UnityEngine.XR;

public class PlayerJump : MonoBehaviour
{
    public float moveForce = 1f;        // The force applied when jumping
    //public float triggerThreshold = 0.8f; // The threshold for the trigger press to initiate jump
    public bool canRise = true;         // Flag to control jumping ability
    public bool canSink = false;         // Flag to control jumping ability
    private PlayerGravity playerGravity;

    void Start()
    {
        playerGravity = GetComponent<PlayerGravity>(); // Correctly assign the component

        //Debug to check if PlayerGravity script is on the same object
        if (playerGravity == null)
        {
            Debug.LogError("PlayerGravity component not found on the GameObject: " + gameObject.name);
        }
        else
        {
            Debug.Log("PlayerGravity component successfully found on the GameObject: " + gameObject.name);
        }
    }

    void Update()
    {
        if (playerGravity != null)
        {
            // Check if the player wants to rise/ sink
            if (AButtonPressed())
            {
                Rise();
                //Debug.Log("Rise");
            }
            if (!playerGravity.isGrounded && BButtonPressed())
            {
                Sink();
                //Debug.Log("Sink");
            }
        }
    }

    bool AButtonPressed()
    {
        // Get the active input device (left and right hand controllers)
        //InputDevice leftHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Check if the trigger is pressed on either controller
        //bool leftTriggerPressed = leftHandDevice.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue) && leftTriggerValue > triggerThreshold;
        bool aButtonPressed = rightHandDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue;

        //return true if B button is pressed
        //return leftTriggerPressed || rightTriggerPressed;
        return aButtonPressed;
    }

    bool BButtonPressed()
    {
        // Get the active input device (left and right hand controllers)
        InputDevice rightHandDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        // Check if the trigger is pressed on either controller
        bool bButtonPressed = rightHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButton) && secondaryButton;

        //return true if B button is pressed
        return bButtonPressed;
    }        
    void Rise()
    {
        //Apply rising
        //Vector3 new(Vector3.up * jumpForce, ForceMode.Impulse);
        Vector3 riseMovement = new Vector3(0, moveForce*Time.deltaTime, 0);
        transform.position += riseMovement;
    }

    void Sink()
    {
        //Apply sinking
        //Vector3 new(Vector3.up * jumpForce, ForceMode.Impulse);
        Vector3 sinkMovement = new Vector3(0, moveForce*Time.deltaTime, 0);
        transform.position -= sinkMovement;
    }
}