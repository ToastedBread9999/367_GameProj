using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private NextScene nextScene;  // Reference to the PlayerCounter instance

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the PlayerCounter component is found and itemCount is greater than or equal to 3
        if (other.CompareTag("Player"))
        {
            // Load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
} 
