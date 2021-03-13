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
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    // public GameObject lootDrop;

    [SerializeField]
    private int enemyGiveDamage = 1;
    private HealthSystem playerHealth;
    // seconds before hurting again
    private float beforeNextDamage = 3f;
    private bool isColliding;

    void Start(){
        playerHealth = FindObjectOfType<HealthSystem>();
    }

    void Update(){
        if (isColliding)
        {
            beforeNextDamage -= Time.deltaTime;

            if (beforeNextDamage <= 0)
            {
                playerHealth.Damage(enemyGiveDamage);
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

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<HealthSystem>().Damage(enemyGiveDamage);
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
