using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent (typeof(PlayerInput))]
public class Movement : MonoBehaviour
{
    // Define all of the assets and variables
    private Rigidbody2D rb;
    private SpriteRenderer sprite_renderer;
    private Animator animator;
    public float JumpingForce = 5f;
    public float Speed = 8f;
    private float horizontal;
    private bool IsGrounded=true;

    // Awake Function
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    // Update Function
    private void FixedUpdate()
    {
        if (horizontal == 1) { sprite_renderer.flipX = false; }
        else if (horizontal == -1) { sprite_renderer.flipX = true; }
        else if (horizontal == 0) { animator.SetBool("IsRunning", false); }
        rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);
    }

    // Checking If object is touching the ground or not

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") IsGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") IsGrounded = false;
    }

    // Function to Jump
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded)
        { 
            rb.velocity = new Vector2(rb.velocity.x, JumpingForce);
        }
    }
    
    // Function TO MOVE
    public void Move(InputAction.CallbackContext context) 
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
