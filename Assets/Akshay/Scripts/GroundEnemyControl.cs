using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BoxCollider2D))]
[RequireComponent (typeof(SpriteRenderer))]

public class GroundEnemyControl : MonoBehaviour
{
    private Rigidbody2D EnemyRb;
    private SpriteRenderer spriteRenderer;
    private GameObject Player;
    private GroundEnemyAnimation ToCheckIfEnemyAttacks;

    // PLACEHOLDER FLOATS
    private float EnemyDirection;
    [SerializeField]
    private float EnemySpeed;
    [SerializeField]
    private float ForceValue= 5f;

    // AWAKE FUNCTION
    private void Awake()
    {
        EnemyRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ToCheckIfEnemyAttacks = GetComponent<GroundEnemyAnimation>();
    }
    private void FixedUpdate()
    {
        // Player Detection using raycast
        RaycastHit2D PlayerDetectionL = Physics2D.Linecast(new Vector3(EnemyRb.position.x-6f, EnemyRb.position.y,0f), new Vector3(EnemyRb.position.x - 0.5f, EnemyRb.position.y,0f));
        RaycastHit2D PlayerDetectionR = Physics2D.Linecast(new Vector3(EnemyRb.position.x+6f, EnemyRb.position.y,0f), new Vector3(EnemyRb.position.x + 0.5f, EnemyRb.position.y,0f));
        if (PlayerDetectionL.collider != null || PlayerDetectionR.collider != null)
        {
            if (PlayerDetectionL.collider.tag == "Player" || PlayerDetectionR.collider.tag == "Player")
            {
                Enemy();
            }
        }
    }

    // FOLLOW THE PLAYER OBJECT
    private void Enemy()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyDirection = ((Player.transform.position.x - EnemyRb.transform.position.x) / Mathf.Abs(Player.transform.position.x - EnemyRb.transform.position.x));
        //Debug.Log(Mathf.Abs(Player.transform.position.x - EnemyRb.transform.position.x));
        if (Mathf.Abs(Player.transform.position.x - EnemyRb.transform.position.x) > 1.5f)
        {
            EnemyRb.velocity = new Vector2(EnemySpeed * EnemyDirection, 0f);
        }
        else
        {
            EnemyRb.velocity = Vector2.zero;
        }
        Flip();
        if (ToCheckIfEnemyAttacks.EnemyAttacking)
        {
            EnemyRb.velocity = Vector2.zero;
            EnemyRb.AddForce(ForceValue *Vector2.right * EnemyDirection, ForceMode2D.Impulse);
        }
    }
    //FLIP THE SPRITE IF NEEDED
    private void Flip()
    {
        if (EnemyRb.velocity.x < 0) spriteRenderer.flipX = true;
        else if (EnemyRb.velocity.x > 0) spriteRenderer.flipX = false;
    }
}
