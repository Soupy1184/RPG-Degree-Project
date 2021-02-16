using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sidescrolling_PlayerController : MonoBehaviour
{
     //Start() variables
     private Rigidbody2D rb;
     private Animator anim;
     private Collider2D coll;

     //Finite State Machine (FSM) for animations
     private enum State { idle, running, jumping, falling, attack1, attack2, attack3, hurt };
     private State state = State.idle;

     //Unity inspector variables
     [SerializeField] private LayerMask ground;
     [SerializeField] private float speed = 5f;
     [SerializeField] private float jumpForce = 8f;
     [SerializeField] private int money = 0;
     [SerializeField] private Text moneyCountText;

     private float attackCooldown = 0.5f;
     private float attackTimer = 0.0f;

     private float hurtCooldown = 0.5f;
     private float hurtTimer = 0.0f;

     //for attacking enemies
     public Transform attackPoint1, attackPoint2, attackPoint3;
     public float attackRange1, attackRange2, attackRange3;
     public LayerMask enemyLayers;
     public int attackDamage = 20;

     public int maxHealth = 100;
     private int currentHealth;
    // private bool isHurt;

     // Start is called before the first frame update
     private void Start()
     {
          currentHealth = maxHealth;
          rb = GetComponent<Rigidbody2D>();
          anim = GetComponent<Animator>();
          coll = GetComponent<Collider2D>();
     }

    // Update is called once per frame
     private void Update()
     {
          //this is used for the attack cooldown
          attackTimer += Time.deltaTime;
          //this is used for the player delay when getting hit
          hurtTimer += Time.deltaTime;

          if (hurtTimer > hurtCooldown) {
               //if you are not attacking, then you can have movement animations (states)
               if (attackTimer > attackCooldown) {
                    VelocityState();
               }
               //runs every frame to manage user inputs
               Controller();
          }

          //use Enumerator 'state' to set the current animation.
          anim.SetInteger("state", (int)state);
     }


     private void OnTriggerEnter2D(Collider2D collision) {
          if (collision.tag == "Money") {
               Destroy(collision.gameObject);
               money += 1;
               moneyCountText.text = money.ToString();
          }
     }

     private void Controller() {
          float hDirection = Input.GetAxis("Horizontal");

          //if the player is currently attacking, go into this if statement. 
          //The "else" below manages movement, which is disabled if you're attacking
          if (attackTimer <= attackCooldown) {
               //this second if represents a window between being "done" attacking and beginning another attack, to combo
               if (attackTimer > attackCooldown - 0.1f) {
                    //transition from "attack1" to "attack2" animations
                    if (Input.GetKey("j") && state == State.attack1) {
                        // print("Attack 2");
                         state = State.attack2;
                         attackTimer -= 0.4f;
                         StartCoroutine(AttackEnum(0.3f));
                    }
                    //transition from "attack2" to "attack3" animations
                    else if (Input.GetKey("j") && state == State.attack2) {
                        // print("Attack 3");
                         state = State.attack3;
                         attackTimer -= 0.4f;
                         StartCoroutine(AttackEnum(0.3f));
                    }
                    //transition from "attack3" back to the "attack1" animation
                    else if (Input.GetKey("j") && state == State.attack3) {
                        // print("Attack 1 repeat");
                         state = State.attack1;
                         attackTimer -= 0.4f;
                         StartCoroutine(AttackEnum(0.3f));
                    }
               }
          }



          //if currently attacking, unable to move.
          else {

               //If you press the "left" movement key, move left and face the player to the left
               if (hDirection < 0) {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                    transform.localScale = new Vector2(-1, 1);
               }
               //If you press the "right" movement key, move right and face the player to the right
               else if (hDirection > 0) {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                    transform.localScale = new Vector2(1, 1);
               }

               //If you press the "jump" key and aren't on the ground, jump and animate jumping
               if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)) {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    state = State.jumping;
               }

               //if you press the attack key, then begin the attack animation and set the attack cooldown timer
               if (Input.GetKey("j") && coll.IsTouchingLayers(ground)) {
                    print("Attack 1");
                    state = State.attack1;
                    attackTimer = 0;
                    StartCoroutine(AttackEnum(0.3f));
               }

          }
     }

     private void VelocityState() {
          if(state == State.jumping) {
               if (rb.velocity.y < .1f) {
                    state = State.falling;
               }
          }
          else if (state == State.falling) {
               if (coll.IsTouchingLayers(ground)) {
                    state = State.idle;
               }
          }

          else if (Mathf.Abs(rb.velocity.x) > 2f) {
               state = State.running;
          }

          else {
               state = State.idle;
          }
     }

     private IEnumerator AttackEnum(float waitTime) {
          yield return new WaitForSeconds(waitTime);

          Collider2D[] hitEnemies;

          if (state == State.attack1) {
               //Detect enemies in range of attack 1
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange1, enemyLayers);
          }

          else if (state == State.attack2) {
               //Detect enemies in range of attack 2
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, enemyLayers);
          }

          else {
               //Detect enemies in range of attack 3
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint3.position, attackRange3, enemyLayers);
          }

          //deal damage to the detected enemies and flashes it red
          foreach (Collider2D enemy in hitEnemies) {
               Debug.Log("We hit " + enemy.name);
               enemy.GetComponent<Sidescrolling_EnemyHealthManager>().TakeDamage(attackDamage);
               enemy.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
               StartCoroutine(FixColour(enemy));
          }

          //turn detected enemies to face player and push back slightly
          foreach (Collider2D enemy in hitEnemies) {
               if (enemy.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                    enemy.transform.localScale = new Vector2(-1, 1);
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0f);
               }
               else if (enemy.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                    enemy.transform.localScale = new Vector2(1, 1);
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0f);
               }
          }
     }

     private IEnumerator FixColour(Collider2D enemy) {
          yield return new WaitForSeconds(0.1f);
          //Debug.Log("Fixing " + enemy.name + " colour");
          enemy.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
     }


     public void TakeDamage(int damage) {
          currentHealth -= damage;

          //play hurt animation
          anim.SetTrigger("Hurt");

          //this is used to stop the player from moving for a moment when hit
          //isHurt = true;

          if (currentHealth <= 0) {
               Die();
          }
          else {
               hurtTimer = 0f;
               //StartCoroutine(HurtDelay(0.3f));
          }
     }


     void Die() {
          Debug.Log("Player died!");

          //Die animation
          anim.SetBool("Dead", true);

          //disable the enemy

          /*
          GetComponent<Rigidbody2D>().gravityScale = 0;
          GetComponent<Collider2D>().enabled = false;
          rb.velocity = new Vector2(0, 0);
          this.enabled = false;*/

          StartCoroutine(DieDelay());

          //  Destroy(this.gameObject);
     }

     private IEnumerator DieDelay() {
          yield return new WaitForSeconds(0.5f);

          GetComponent<Rigidbody2D>().gravityScale = 0;
          GetComponent<Collider2D>().enabled = false;
          rb.velocity = new Vector2(0, 0);
          this.enabled = false;
     }
     /*
     private IEnumerator HurtDelay(float waitTime) {
          yield return new WaitForSeconds(waitTime);
          Debug.Log("Player finished hitstun");
          isHurt = false;
     }*/


     void OnDrawGizmosSelected() {
          if (attackPoint1 == null) {
               return;
          }
          Gizmos.DrawWireSphere(attackPoint3.position, attackRange3);
     }

}