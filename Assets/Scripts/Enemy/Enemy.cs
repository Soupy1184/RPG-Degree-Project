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
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject lootDrop;

    // take initiali value of health when awake
    private void Awake(){
        health = maxHealth.initialValue;
    }

    // take damage when attacked
    private void TakeDamage(float damage){
        health -= damage;

        // dies if out of health
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }

    //method to call coroutine
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }
    
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime){
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);

            // velocity to 0
            myRigidbody.velocity = Vector2.zero;

            //reset state
            currentState = EnemyState.idle;
        }
    }
}
