using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // Reference to the TextMeshProUGUI component
    public float timeRemaining;
    private bool timerIsRunning = false;

    void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timeRemaining = 60; // Set the timer to 60 seconds (1 minute)
    }

    //Keep counting down
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                //Go to fail screen 
                SceneManager.LoadScene(4); 
            }
        }
    }

    //Update the timer display
    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
