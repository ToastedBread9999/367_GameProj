using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class OxygenTank : MonoBehaviour
{
    public XRSocketInteractor oxygenSocket;
    public bool isAttached = false;
    public float timeBeforeDestruction = 10.0f; // Time in seconds before the oxygen tank is destroyed after being picked up
    public float timeBeforeDisable = 2.0f; // Time in seconds before the oxygen tank socket is disabled
    private bool hasCollided = false; // Flag to check if the collision has been registered
    bool triggerHeld = false;

    private Coroutine disableCoroutine;

    void Start()
    {
        if (oxygenSocket == null)
        {
            Debug.LogError("XRSocketInteractor not assigned to OxygenTank script.");
            return;
        }

        // Initially disable the socket interactor
        oxygenSocket.enabled = false;
    }

    void Update(){
        triggerHeld = InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValue) && triggerValue > 0.1f ||
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.grip, out float triggerValueTwo) && triggerValueTwo > 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oxygen")&& !hasCollided && triggerHeld)
        {
            isAttached = true;
            hasCollided = true;

            oxygenSocket.enabled = true;

            // Notify the player that this canister is attached
            PlayerManager.Instance.AttachTanks(this);

            // Find the Canvas GameObject (assuming it's named "Canvas")
            GameObject canvasObj = GameObject.Find("Canvas");

            if (canvasObj != null)
            {
                // Get the TimerManager component from the Canvas
                TimerManager timerManager = canvasObj.GetComponent<TimerManager>();

                if (timerManager != null)
                {
                    // Access the timeRemaining variable from TimerManager
                    timerManager.timeRemaining += 60;
                    float remainingTime = timerManager.timeRemaining;
                    Debug.Log("Added 100s to Timer");
                    Debug.Log("Time remaining on Timer: " + remainingTime);

                    // Start the coroutine to destroy the oxygen tank after a delay
                    StartCoroutine(DestroyAfterDelay());
                }
                else
                {
                    Debug.LogWarning("TimerManager script not found on Canvas.");
                }
            }
            else
            {
                Debug.LogWarning("Canvas GameObject not found.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Oxygen")&& !triggerHeld)
        {
            isAttached = false;
            // Notify the player that this canister is detached
            PlayerManager.Instance.DetachTank(this);

            oxygenSocket.enabled = false;

            // Stop the destruction coroutine if the player detaches the tank
            disableCoroutine = StartCoroutine(DisableSocketWithDelay(timeBeforeDisable));
        }
    }

    private IEnumerator DisableSocketWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        oxygenSocket.enabled = true;
    }

    private IEnumerator DestroyAfterDelay()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(timeBeforeDestruction);

        // Destroy the oxygen tank
        Destroy(gameObject);
    }
}