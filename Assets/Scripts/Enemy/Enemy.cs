/*
    written by: Afieqha Mieza
    knockback concept resource: https://www.youtube.com/watch?v=BLfNP4Sc_iA
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum to store different state of the enemies
public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : LootDropObject
{
    [Header("Base Target")]
    public PlayerController playerHurt;
    [SerializeField]
    private int enemyGiveDamage = 5;
    private float beforeNextDamage = 2f;
    private bool isColliding;
    // private HealthSystem playerHealth;

    [Header("Base Enemy")]
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public float moveSpeed;
    public int baseAttack;


    void Start(){ }

    void Update()
    {
        // check for collision with player 
        // if one attack is given, 
        // update beforeNextDamage
        if (isColliding)
        {
            beforeNextDamage -= Time.deltaTime;

            if (beforeNextDamage <= 0)
            {
                beforeNextDamage = 3f;
            }
        }
    }

    // take initiali value of health when awake
    private void Awake(){
        health = maxHealth.initialValue;
    }

    // take damage when attacked
    private void TakeDamage(float damage)
    {
        health -= damage;

        // dies if out of health
        if (health <= 0)
        {
            // set enemy to inactive
            this.gameObject.SetActive(false);

            // loot drop coins when is dead
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }

    //method to call Knock coroutine
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        // call coroutine
        StartCoroutine(KnockCo(myRigidbody, knockTime));

        // update damage whenever is knocked
        TakeDamage(damage);
    }
    
    // coroutine to knockback whenever player strikes
    // makes knocked enemies knockback a little
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;

            //reset state
            currentState = EnemyState.idle;
        }
    }

    // upon colliding to player, hurt player
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().Hurt(enemyGiveDamage);
        }
    }

    // if player remains colliding, set isColliding
    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
        
    }

    // if player is not colliding anymore, 
    // update isColliding
    // set beforeNextDamage to initial
    private void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            beforeNextDamage = 3f;
        }
    }
}
