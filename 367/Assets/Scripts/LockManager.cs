using UnityEngine;
using System.Collections;

public class LockManager : MonoBehaviour
{
    public string keyName = "Key"; // The name of the key GameObject
    public bool unlocked = false;
    private bool attached = false;

    public float moveDistance = 1.0f; // Distance to move the lock
    public float moveDuration = 1.0f; // Duration of the move

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == keyName && !attached)
        {
            Debug.Log("Key is inserted!");

            // Check if the KeyManager component exists
            KeyManager key = other.GetComponent<KeyManager>();
            if (key != null && key.isPickedUp)
            {
                PlaceKeyInLock(key);
            }
            attached = true;
        } 
        else 
        {
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

        key.isPickedUp = false;
    }

    public void UnlockDoor()
    {
        if(unlocked){
            Debug.Log("Door Unlocked!");
            StartCoroutine(MoveLock());
        }
    }

    private IEnumerator MoveLock()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + Vector3.back * moveDistance;

        float elapsedTime = 1.0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
    }
}