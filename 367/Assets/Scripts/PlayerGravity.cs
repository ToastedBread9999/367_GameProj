using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    //Player Sink speed
    public float sinkSpeed = 0.1f;
    private bool isGrounded = true;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGroundStatus();
        Sink();
    }
    void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

    void Sink()
    {
        if(!isGrounded){
            //Sinking movement
            Vector3 downwardMovement = new Vector3(0, -sinkSpeed * Time.deltaTime, 0);
            transform.position += downwardMovement;
        }
    }
}