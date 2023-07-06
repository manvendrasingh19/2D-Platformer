using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    // Define all of the assets and variables
    private Rigidbody2D rb;
    public GameObject PlayerObject;
    private float JumpingForce = 5f;
    private float Speed = 8f;
    private float horizontal;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float FacingDirection;

    // Awake Function
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update Function
    private void Update()
    {
        if(horizontal == 1) { FacingDirection = 0; }
        else if(horizontal == -1) { FacingDirection = 1; }
        rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);
        PlayerObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 180f * FacingDirection, 0f));
    }
    // Checking If object is touching the ground or not
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    // Function to Jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
            rb.velocity = new Vector2(rb.velocity.x, JumpingForce);
        if (context.canceled && rb.velocity.y > 0f)
            rb.velocity = new Vector2(rb.velocity.x, JumpingForce*0.5f);
    }
    
    // TO MOVE
    public void Move(InputAction.CallbackContext context) 
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
