using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

//method to call coroutine
    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }
    
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime){
        if (myRigidbody != null && currentState != EnemyState.stagger)
        {
            yield return new WaitForSeconds(knockTime);

            // velocity to 0
            myRigidbody.velocity = Vector2.zero;

            //reset state
            currentState = EnemyState.idle;
        }
    }
}
