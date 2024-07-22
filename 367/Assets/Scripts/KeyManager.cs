using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyManager : MonoBehaviour
{
    public bool isPickedUp = false;

    private bool hasCollided = false; // Flag to check if the collision has been registered
    private bool triggerHeld = false; // Check if the trigger is being held
    private bool previousTriggerHeld = false; // To store the previous state of the trigger

    public XRSocketInteractor keySocket;
    public XRGrabInteractable grabInteractable;

    public float delayBeforeDisable = 2.0f; // Adjust the delay time as needed
    //private Coroutine disableCoroutine;


    void Start()
    {
        if (keySocket == null)
        {
            Debug.LogError("XRSocketInteractor not assigned to OxygenTank script.");
            return;
        }

        // Initially disable the socket interactor
        keySocket.enabled = false;
    }


    void Update()
    {
        // Check the trigger if it is pressed
        triggerHeld = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValue) && triggerValue > 0.01f ||
                      InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValueTwo) && triggerValueTwo > 0.01f;

        // Check if the trigger was released
        if (previousTriggerHeld && !triggerHeld)
        {
            Debug.Log("Trigger Released");
            // Call your method here when the trigger is released
        }

        // Update the previous state
        previousTriggerHeld = triggerHeld;
    }

    //When the Key collides with the Player or the Lock
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& !hasCollided && triggerHeld){

            //Debug.Log("Key is picked up");
            isPickedUp = true;


        } else if(other.CompareTag("Lock")&& !hasCollided && triggerHeld){
            //Debug.Log("Key is colliding with lock");
            hasCollided = true;
        }
    }

    //When the Key is still colliding with the Player or the Lock
    //Enable the socket
    private void OnTriggerStay(Collider other){
        if(other.CompareTag("Player") && hasCollided && triggerHeld){
            //Debug.Log("Key is being held");
            keySocket.enabled = true;
        } else if(other.CompareTag("Lock")&& hasCollided && !triggerHeld){
            //Debug.Log("Key is on the lock");
            keySocket.enabled = false;
        }
    }

    //When the Key exits the Player or the Lock colliders
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lock") && hasCollided && !triggerHeld && previousTriggerHeld)
        {
            hasCollided = false;
            keySocket.enabled = true;

            // Unlock the 'door' when key is released and attached
            LockManager lockManager = other.GetComponent<LockManager>();
            if (lockManager != null)
            {
                lockManager.UnlockDoor();
            }
            else
            {
                Debug.LogError("LockManager component not found on the Lock object.");
            }

            // Start the disable coroutine with delay
            StartCoroutine(DisableSocketWithDelay(delayBeforeDisable));
        }
        else if (other.CompareTag("Player") && hasCollided && !triggerHeld)
        {
            // Debug.Log("Key dropped");
        }
    }

    //Disable the socket
    private IEnumerator DisableSocketWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        keySocket.enabled = false;
        grabInteractable.enabled = false;
    }

}
