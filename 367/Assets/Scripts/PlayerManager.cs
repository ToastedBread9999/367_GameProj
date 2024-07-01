using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Hands.Samples.Gestures.DebugTools;


public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; // Singleton instance

    public Text timerText;
    private float timer = 0f;
    private bool isTimerRunning = false;
    private List<OxygenTank> attachedTanks = new List<OxygenTank>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (attachedTanks.Count > 0)
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }

        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    public void AttachTanks(OxygenTank canister)
    {
        if (!attachedTanks.Contains(canister))
        {
            attachedTanks.Add(canister);
        }
    }

    public void DetachTank(OxygenTank canister)
    {
        if (attachedTanks.Contains(canister))
        {
            attachedTanks.Remove(canister);
        }
    }

    void StartTimer()
    {
        isTimerRunning = true;
    }

    void StopTimer()
    {
        isTimerRunning = false;
    }

    void UpdateTimerUI()
    {
        //timerText.text = "Timer: " + timer.ToString("F2") + "s";
    }
}