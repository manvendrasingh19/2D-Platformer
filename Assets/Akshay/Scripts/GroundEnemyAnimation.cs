using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class GroundEnemyAnimation : MonoBehaviour
{
    // ALL DECLARATIONS
    private Animator animator;
    private Rigidbody2D EnemyRb;
    private bool IsWalking;
    public bool EnemyAttacking;
    private float attackDelay;

    //NAME OF ANIMATIONS STORED IN FORM OF STRINGS
    const string ENEMY_IDLE = "BlueWizard_IDLE";
    const string ENEMY_WALKING = "BlueWizard_WALKING";
    const string ENEMY_ATTACKING = "BlueWizard_DASH";

    private void Start()
    {
        animator = GetComponent<Animator>();
        EnemyRb = GetComponent<Rigidbody2D>();
        EnemyAttacking = false;
    }

    private void Update()
    {
        RaycastHit2D AttackDetectionL = Physics2D.Linecast(new Vector3(EnemyRb.position.x - 1.5f, EnemyRb.position.y, 0f), new Vector3(EnemyRb.position.x - 0.5f, EnemyRb.position.y, 0f));
        RaycastHit2D AttackDetectionR = Physics2D.Linecast(new Vector3(EnemyRb.position.x + 1.5f, EnemyRb.position.y, 0f), new Vector3(EnemyRb.position.x + 0.5f, EnemyRb.position.y, 0f));
        
        // IsWalking OR NOT
        if ((int)Mathf.Abs(EnemyRb.velocity.x) > 0) IsWalking = true;
        else IsWalking = false;
        
        // ATTACK LOGIC
        if ((AttackDetectionL.collider != null || AttackDetectionR.collider != null) && !EnemyAttacking)
        {
            if (AttackDetectionL.collider.tag == "Player" || AttackDetectionR.collider.tag == "Player")
            {
                EnemyAttacking = true;
                animator.Play(ENEMY_ATTACKING);
                attackDelay = animator.GetCurrentAnimatorClipInfo(0).Length;
                Invoke("Attacking", attackDelay);
            }
        }
        // LOGIC
        if (!IsWalking && !EnemyAttacking)
        {
            animator.Play(ENEMY_IDLE); 
        }
        else if (IsWalking && !EnemyAttacking)
        {
            animator.Play(ENEMY_WALKING);
        }
    }
    private void Attacking()
    {
        EnemyAttacking = false;
    }
}
