using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TotalTiming : MonoBehaviour
{
    public static TotalTiming Instance { get; private set; }

    public TextMeshProUGUI timerText; // UI Text to display the timer
    private float elapsedTime = 0f;
    private bool isGameFinished = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
            Debug.Log("Retained");
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find and assign the timerText reference in the new scene
        timerText = GameObject.FindWithTag("TimerText")?.GetComponent<TextMeshProUGUI>();
        UpdateTimerUI(); // Update the UI to reflect the current elapsed time
    }

    private void Update()
    {
        if (!isGameFinished)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log("Plus one");
            UpdateTimerUI();
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void GameFinished()
    {
        isGameFinished = true;
        Debug.Log("Game Finished! Total Time: " + elapsedTime);
    }
}