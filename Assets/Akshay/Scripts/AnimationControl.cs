using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class AnimationControl : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool IsRunning, IsJumpingUP, IsJumpingDOWN;
    private bool IsGrounded = true;
    private bool PlayerAttacking = false;
    private float attackDelay;
    
    //NAME OF ANIMATIONS STORED IN FORM OF STRINGS

    const string PLAYER_IDLE = "PlayerChar_IDLE";
    const string PLAYER_RUNNING = "PlayerChar_RUNNING";
    const string PLAYER_DYING = "PlayerChar_DYING";
    const string PLAYER_JUMPUP = "PlayerChar_jumpingUP";
    const string PLAYER_JUMPDOWN = "PlayerChar_jumpingDOWN";
    const string PLAYER_SWINGING = "PlayerChar_SWORD";

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        //IsRunning
        if ((int)Mathf.Abs(rb.velocity.x) > 0) { IsRunning = true; }
        else { IsRunning = false; }
        //IsJumping UP AND DOWN
        
        if ((int)rb.velocity.y > 0) { IsJumpingUP = true;}
        else if ((int)rb.velocity.y<0) { IsJumpingDOWN = true;}
        else if ((int)rb.velocity.y == 0) { IsJumpingUP = false; IsJumpingDOWN = false; }
        //LOGIC
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            PlayerAttacking = true;
            animator.Play(PLAYER_SWINGING);
            attackDelay = animator.GetCurrentAnimatorClipInfo(0).Length;
            Invoke("SwingOrNot", attackDelay-0.4f); //0.4f is just so animation ends quicker and looks smoother...
        }
        else if (!IsRunning && !IsJumpingUP && !IsJumpingDOWN && IsGrounded && !PlayerAttacking) animator.Play(PLAYER_IDLE);
        else if (IsGrounded && (int)rb.velocity.y == 0 && IsRunning && !PlayerAttacking) animator.Play(PLAYER_RUNNING);
        else if (!IsGrounded && IsJumpingUP && !IsJumpingDOWN && !PlayerAttacking) animator.Play(PLAYER_JUMPUP);
        else if (!IsGrounded && IsJumpingDOWN && !IsJumpingUP && !PlayerAttacking) animator.Play(PLAYER_JUMPDOWN);
        
    }
    // TO SEE IF OBJECT IS GROUNDED
    private void SwingOrNot()
    {
        PlayerAttacking = !PlayerAttacking;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") IsGrounded = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") IsGrounded = false;
    }
}