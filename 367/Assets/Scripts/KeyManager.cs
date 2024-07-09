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
        triggerHeld = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValue) && triggerValue > 0.1f ||
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValueTwo) && triggerValueTwo > 0.1f;
    }


    // Add additional logic for picking up and dropping the key
    // For example, when the key is picked up by the player:

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&& !hasCollided && triggerHeld){
            //Debug.Log("Collided Hand");
            PickUp();
            hasCollided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")&& hasCollided && !triggerHeld){
            //Debug.Log("Exit Collided Hand");
            Drop();
            hasCollided = false;

            // Start the disable coroutine with delay
            disableCoroutine = StartCoroutine(DisableSocketWithDelay(delayBeforeDisable));
        
        }
        
    }

    private IEnumerator DisableSocketWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        keySocket.enabled = true;
    }

    public void PickUp()
    {
        isPickedUp = true;
        //GetComponent<Collider>().enabled = false; // Disable collider to prevent further interactions
        Debug.Log("Key is picked up");
    }

    public void Drop()
    {
        isPickedUp = false;
        //GetComponent<Collider>().enabled = true; // Re-enable collider
        Debug.Log("Key is dropped");
    }

    public void Used()
    {
        isPickedUp = false;
        //GetComponent<Collider>().enabled = true; // Re-enable collider
        Debug.Log("Key is used up");
    }
}
