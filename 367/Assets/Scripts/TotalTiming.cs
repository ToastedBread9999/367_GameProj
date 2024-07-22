using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TotalTiming : MonoBehaviour
{
    public static TotalTiming Instance { get; private set; }

    public TextMeshProUGUI timerText; // UI Text to display the timer

    private static float elapsedTime = 0f; // Static variable to store elapsed time
    private float finalTime;
    private static bool isGameFinished = false;

    // private void Awake()
    // {
    //     if (Instance != null && Instance != this)
    //     {
    //         Destroy(gameObject);
    //         Debug.Log("Destroyed");
    //     }
    //     else
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //         SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    //         Debug.Log("Retained");
    //     }
    // }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
        }
    }

    //When the scene is loaded 
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and assign the timerText reference in the new scene
        timerText = GameObject.FindWithTag("TimerText")?.GetComponent<TextMeshProUGUI>();
        UpdateTimerUI(); // Update the UI to reflect the current elapsed time

        //If the game was restarted
        if(SceneManager.GetActiveScene().name == "Tutorial"){
            GameStart();
        }
    }

    //Update the timer with the elapsed time
    private void Update()
    {
        if (!isGameFinished)
        {
            elapsedTime += Time.deltaTime;
            //Debug.Log("Plus one :" + elapsedTime);
            UpdateTimerUI();
        } else {
            finalTime = elapsedTime;

            int minutes = Mathf.FloorToInt(finalTime / 60);
            int seconds = Mathf.FloorToInt(finalTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    //Display the timer 
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    //Stop the timer
    public void GameFinished()
    {
        //Note the timing and pass it
        finalTime = elapsedTime;
        isGameFinished = true;

        Debug.Log("Game Finished! Total Time: " + elapsedTime);
    }

    //Stop the timer
    public void GameStart()
    {
        //Note the timing and pass it
        finalTime = 0;
        elapsedTime = 0;
        isGameFinished = false;

        Debug.Log("Game Started!");
    }
}
