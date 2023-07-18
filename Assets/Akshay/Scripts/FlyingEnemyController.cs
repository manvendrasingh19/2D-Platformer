using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEditor.Timeline.Actions;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(SpriteRenderer))]

public class FlyingEnemyController : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D frb;
    private SpriteRenderer spriteRenderer;
    private Vector2 startPosition;
    private Vector2 currentPos;
    private int x_Dir = 0, y_Dir = 0;
    [SerializeField] private float EnemySpeed;
    public bool chase = false;
    public bool returnToStart = false;
    [SerializeField] private bool IsAttacking;
    
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        frb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        frb.isKinematic = true;
        frb.freezeRotation = true;
        startPosition = transform.position;
        currentPos = transform.position;
    }
    private void FixedUpdate()
    {
        if(Player != null && chase)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, EnemySpeed * Time.deltaTime);
        }
        if (returnToStart)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPosition, EnemySpeed * Time.deltaTime);
        }
        if (transform.position.x == startPosition.x && transform.position.y == startPosition.y)
        { 
            returnToStart = false;
            chase = false;
        }

        // TO CHECK WHICH DIRECTION IS THE FENEMY IS FACING
        if (transform.position.x != currentPos.x || transform.position.y != currentPos.y)
        {
            if (transform.position.x > currentPos.x) x_Dir = 1;
            if (transform.position.y > currentPos.y) y_Dir = 1;
            if (transform.position.x < currentPos.x) x_Dir = -1;
            if (transform.position.y < currentPos.y) y_Dir = -1;
            if (transform.position.x == currentPos.x) x_Dir = 0;
            if (transform.position.y == currentPos.y) y_Dir = 0;
        }
        currentPos = transform.position;
        Flip();
    }

    void Flip()
    {
        if (x_Dir == -1)
            spriteRenderer.flipX = true;
        if (x_Dir == +1)
            spriteRenderer.flipX = false;
    }
}
