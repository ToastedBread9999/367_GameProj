using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void StartBtn()
    {
        SceneManager.LoadScene("Scene1"); // Replace with your actual scene name
    }
   

    public void QuitGame()
    {
        Application.Quit();
    }
}
