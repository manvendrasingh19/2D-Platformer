using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class FlyingEnemyAnimation : MonoBehaviour
{
    // ALL DECLARATIONS
    private Animator animator;
    private FlyingEnemyController controller;

    //NAME OF ANIMATIONS STORED IN FORM OF STRINGS
    const string FENEMY_IDLE = "FlyingEnemy_IDLE";
    const string FENEMY_DYING = "FlyingEnemy_DYING";
    const string FENEMY_ATTACKING = "FlyingEnemy_DASH";
    private void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<FlyingEnemyController>();
    }

    private void Update()
    {
        if (controller.chase == true ||  controller.returnToStart == true)
        {
            animator.Play(FENEMY_ATTACKING);
        }
        else if(controller.chase == false || controller.returnToStart == false) 
        {
            animator.Play(FENEMY_IDLE);
        }
    }
}
