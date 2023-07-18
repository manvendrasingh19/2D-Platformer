using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class FlyingEnemyTrigger : MonoBehaviour
{
    private BoxCollider2D m_Collider;
    public FlyingEnemyController[] fEnemies;
    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        m_Collider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach (FlyingEnemyController fEnemy in fEnemies)
            {
                fEnemy.chase = true;
                fEnemy.returnToStart = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (FlyingEnemyController fEnemy in fEnemies)
            {
                fEnemy.chase = false;
                fEnemy.returnToStart = true;
            }
        }
    }

}
