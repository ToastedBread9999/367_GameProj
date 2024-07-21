using UnityEngine;

public class Mouse : MonoBehaviour
{
    void Start()
    {
        // Show the cursor
        Cursor.visible = true;
        // Unlock the cursor
        Cursor.lockState = CursorLockMode.None;
    }
}
