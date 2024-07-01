using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTank : MonoBehaviour
{
    public bool isAttached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Oxygen"))
        {
            isAttached = true;
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
                    timerManager.timeRemaining += 100;
                    float remainingTime = timerManager.timeRemaining;
                    Debug.Log("Added 100s to Timer");
                    Debug.Log("Time remaining on Timer: " + remainingTime);
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
        if (other.CompareTag("Oxygen"))
        {
            isAttached = false;
            // Notify the player that this canister is detached
            PlayerManager.Instance.DetachTank(this);
        }
    }
}