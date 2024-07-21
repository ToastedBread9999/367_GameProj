using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene("Scene1"); // Replace with your actual scene name
    }

    public void LevelOneSelection()
    {
        SceneManager.LoadScene("Scene1"); 
    }

    public void LevelTwoSelection()
    {
        SceneManager.LoadScene("Scene2");
    }

    public void LevelThreeSelection()
    {
        SceneManager.LoadScene("Scene3");
    }

    public void TutorialStage()
    {
        SceneManager.LoadScene("Tutorial Stage");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void QuitGame()
    {
        Debug.Log("Quit");

#if UNITY_EDITOR
        // If running in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a standalone build
        Application.Quit();
#endif
    }
}
