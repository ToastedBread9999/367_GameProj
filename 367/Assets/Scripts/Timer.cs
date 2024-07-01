using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;
    public OxygenTank oxygenTank; // Reference to the oxygen canister
    private float timer = 0f;
    private bool isTimerRunning = false;

    void Start()
    {
        if (oxygenTank == null)
        {
            Debug.LogError("OxygenTank reference is not assigned.");
        }
    }

    void Update()
    {
        if (oxygenTank.isAttached)
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
        timerText.text = "Timer: " + timer.ToString("F2") + "s";
    }
}
