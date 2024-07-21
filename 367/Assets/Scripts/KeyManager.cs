using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyManager : MonoBehaviour
{
    public bool isPickedUp = false;
    
    bool hasCollided = false; // Flag to check if the collision has been registered

    bool triggerHeld = false;

    public XRSocketInteractor keySocket;
    private XRGrabInteractable grabInteractable;

    public float delayBeforeDisable = 2.0f; // Adjust the delay time as needed
    private Coroutine disableCoroutine;


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


    void Update(){

        //Check the trigger if it is pressed
        triggerHeld = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValue) && triggerValue > 0.01f ||
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValueTwo) && triggerValueTwo > 0.01f;
    }

    //When the Key collides with the Player or the Lock
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& !hasCollided && triggerHeld){

            //Debug.Log("Key is picked up");

            hasCollided = true;
        } else if(other.CompareTag("Player")&& !hasCollided && triggerHeld){
            //Debug.Log("Key is colliding with lock");
        }
    }

    //When the Key is still colliding with the Player or the Lock
    private void OnTriggerStay(Collider other){
        if(other.CompareTag("Player") && hasCollided && triggerHeld){
            //Debug.Log("Key is being held");
            keySocket.enabled = true;
        } else if(other.CompareTag("Lock")&& hasCollided && triggerHeld){
            //Debug.Log("Key is on the lock");
        }
    }

    //When the Key is exits the Player or the Lock colliders
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Lock")&& hasCollided && !triggerHeld){
            //Debug.Log("Key is left on the lock");
            hasCollided = false;
            keySocket.enabled = true;

            // Start the disable coroutine with delay
            disableCoroutine = StartCoroutine(DisableSocketWithDelay(delayBeforeDisable));
        } else if(other.CompareTag("Player")&& hasCollided && !triggerHeld){
            //Debug.Log("Key dropped");
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
