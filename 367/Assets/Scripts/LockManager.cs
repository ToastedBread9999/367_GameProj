using UnityEngine;

public class LockManager : MonoBehaviour
{
    public string keyName = "Key"; // The name of the key GameObject
    private bool attached = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == keyName && !attached)
        {
            Debug.Log("Key is inserted!");

            // Check if the KeyManager component exists
            KeyManager key = other.GetComponent<KeyManager>();
            if (key != null && key.isPickedUp)
            {
                key.Used();
                PlaceKeyInLock(key);
            }
            attached = true;
        } else {
            Debug.Log("Key already inserted");
        }
    }

    private void PlaceKeyInLock(KeyManager key)
    {
        Debug.Log("Attempting to place key in lock...");
        
        // Optional: Play a sound or animation
        // AudioSource.PlayClipAtPoint(lockSound, transform.position);

        // Unlock the door or perform another action
        UnlockDoor();
    }

    private void UnlockDoor()
    {
        // Implement the logic to unlock the door
        // For example, disable a door's collider and open its animation
        Debug.Log("Door Unlocked!");
    }
}
