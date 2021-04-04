using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    void Start(){
        // playerHealth = FindObjectOfType<HealthSystem>();
        // playerHurt = FindObjectOfType<PlayerController>();
    }

    void Update(){
        // check for collision with player 
        if (isColliding)
        {
            beforeNextDamage -= Time.deltaTime;

            if (beforeNextDamage <= 0)
            {
                // // playerHealth.Damage(enemyGiveDamage);
                // playerHurt.Hurt(enemyGiveDamage);
                beforeNextDamage = 3f;
            }
        }
    }

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
            // set enemy to inactive
            this.gameObject.SetActive(false);
            // loot drop coins
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }

    //method to call coroutine
    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        // update damage
        TakeDamage(damage);
    }
    
    // coroutine to knockback whenever player strikes
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

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<PlayerController>().Hurt(enemyGiveDamage);
        }
    }

    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
        
    }

    private void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.CompareTag("Player"))
        {
            isColliding = false;
            beforeNextDamage = 3f;
        }
    }
}
