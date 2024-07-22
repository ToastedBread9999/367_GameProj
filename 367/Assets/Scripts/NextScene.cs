using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private NextScene nextScene;  // Reference to the PlayerCounter instance
    public TotalTiming finished; 

    private void OnTriggerEnter(Collider other)
    {
        // Check if the PlayerCounter component is found and itemCount is greater than or equal to 3
        if (other.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name == "Scene3"){
                finished.GameFinished();
                Debug.Log("Finished Level 3");
            }
                // Load the next scene
                Debug.Log("Next Level");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }

    }
} 
