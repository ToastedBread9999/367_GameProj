using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    //Player Sink speed
    public float sinkSpeed = 0.1f;
    public float groundCheckDistance = 0.5f;

    public bool isGrounded = true;

    public LayerMask groundLayer;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGroundStatus();

        //Player is always sinking
        //Sink();
    }
    //Check if the player is hitting the ground

    public void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    //Sink the player overime
    void Sink()
    {
        if(!isGrounded){
            //Sinking movement
            Vector3 downwardMovement = new Vector3(0, -sinkSpeed * Time.deltaTime, 0);
            transform.position += downwardMovement;
        }
    }
}