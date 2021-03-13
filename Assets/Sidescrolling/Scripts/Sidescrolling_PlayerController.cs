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
     private enum State { idle, running, jumping, falling, attack1, attack2, attack3, hurt, airAttack1, airAttack2, airAttack3};
     private State state = State.idle;

     //Unity inspector variables
     [SerializeField] private LayerMask ground;
     [SerializeField] private float speed = 5f;
     [SerializeField] private float jumpForce = 8f;
     //[SerializeField] private int money = 0;
     //[SerializeField] private Text moneyCountText;

     private float attackCooldown = 0.5f;
     private float attackTimer = 0.0f;

     private float hurtCooldown = 0.3f;
     private float hurtTimer = 0.0f;

     //for attacking enemies
     public Transform attackPoint1, attackPoint2, attackPoint3, attackPoint4, attackPoint5, attackPoint6, attackPoint7;
     public float attackRange1, attackRange2, attackRange3, attackRange4, attackRange5, attackRange6, attackRange7;
     public LayerMask enemyLayers;
     public int attackDamage = 20;

     public int maxHealth = 100;
     private int currentHealth;
     //[SerializeField] private Text healthCountText;
     public PlayerInfo playerInfo;
     public HealthBar healthBar;
     // private bool isHurt;

     //this is for the ground pound attack
     private bool groundPoundDone = true;
     float enemyKBDirection;

     // Start is called before the first frame update
     private void Start()
     {

          currentHealth = maxHealth;
          currentHealth = playerInfo.currentHealth;
          healthBar.SetMaxHealth(playerInfo.maxHealth);
          healthBar.SetHealth(currentHealth);
          rb = GetComponent<Rigidbody2D>();
          anim = GetComponent<Animator>();
          coll = GetComponent<Collider2D>();
     }

    // Update is called once per frame
     private void Update()
     {
          //healthCountText.text = currentHealth.ToString();

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


     // private void OnTriggerEnter2D(Collider2D collision) {
     //      if (collision.tag == "Money") {
     //           Destroy(collision.gameObject);
     //           money += 1;
     //           moneyCountText.text = money.ToString();
     //      }
     // }

     private void Controller() {
          float hDirection = Input.GetAxis("Horizontal");

          //if the player is currently attacking, go into this if statement. 
          //The "else" below manages movement, which is disabled if you're attacking
          if (attackTimer <= attackCooldown) {
               if (groundPoundDone == true) {
                    //this second if represents a window between being "done" attacking and beginning another attack, to combo
                    if (attackTimer > attackCooldown - 0.1f) {
                         //transition from "attack1" to "attack2" animations
                         if (Input.GetKey("j") && state == State.attack1) {
                              print("Attack 2");
                              state = State.attack2;
                              attackTimer -= 0.4f;
                              //StartCoroutine(AttackEnum(0.3f));
                         }
                         //transition from "attack2" to "attack3" animations
                         else if (Input.GetKey("j") && state == State.attack2) {
                              print("Attack 3");
                              state = State.attack3;
                              attackTimer -= 0.4f;
                              //StartCoroutine(AttackEnum(0.3f));
                         }
                         //transition from "attack3" back to the "attack1" animation
                         else if (Input.GetKey("j") && state == State.attack3) {
                              print("Attack 1 repeat");
                              state = State.attack1;
                              attackTimer -= 0.4f;
                              //StartCoroutine(AttackEnum(0.3f));
                         }


                         if (Input.GetKey("j") && Input.GetKey("s") && (state == State.airAttack1 || state == State.airAttack2)) {
                              print("Air Attack 3 (ground pound) in air combo");
                              state = State.airAttack3;
                              //attackTimer = 0;
                              rb.velocity = new Vector2(0, -5);
                              groundPoundDone = false;
                         }
                         else if (Input.GetKey("j") && state == State.airAttack1) {
                              print("Air Attack 2");
                              state = State.airAttack2;
                              attackTimer -= 0.4f;
                              rb.velocity = new Vector2(rb.velocity.x, 4);
                         }
                         else if (Input.GetKey("j") && state == State.airAttack2) {
                              print("Air Attack 1 Repeat");
                              state = State.airAttack1;
                              attackTimer -= 0.4f;
                              rb.velocity = new Vector2(rb.velocity.x, 4);
                         }
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
                    //StartCoroutine(AttackEnum(0.3f));
               }

               //if you press the attack key and down key while in the air, begin the ground slam attack animation
               if (Input.GetKey("j") && Input.GetKey("s") && !coll.IsTouchingLayers(ground) && groundPoundDone == true) {
                    print("Air Attack 3 (Ground Pound)");
                    state = State.airAttack3;
                    //attackTimer = 0;
                    rb.velocity = new Vector3(0, -5);
                    groundPoundDone = false;
               }

               //if you press the attack key WHILE IN THE AIR, then begin aerial attack animation and set attack cooldown timer
               else if (Input.GetKey("j") && !coll.IsTouchingLayers(ground) && groundPoundDone == true) {
                    print("Air Attack 1");
                    state = State.airAttack1;
                    attackTimer = 0;
                    rb.velocity = new Vector2(rb.velocity.x, 4);
               }
          }
     }

     private void VelocityState() {
          if(state == State.jumping || state == State.airAttack1 || state == State.airAttack2) {
               if (rb.velocity.y < .1f) {
                    state = State.falling;
               }
          }
          else if (state == State.falling || state == State.airAttack3) {
               if (coll.IsTouchingLayers(ground)) {
                    if (state == State.airAttack3) {
                         attackTimer = 0.1f;
                         StartCoroutine(groundPoundDelay(0.3f));
                    }
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

     private IEnumerator groundPoundDelay(float waitTime) {
          yield return new WaitForSeconds(waitTime);
          print("Ground Pound Done");
          groundPoundDone = true;
     }

     private IEnumerator AttackEnum(float waitTime) {
          yield return new WaitForSeconds(waitTime);

          Collider2D[] hitEnemies;

          print("checking for collision...");
          if (state == State.attack1) {
               //Detect enemies in range of attack 1
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange1, enemyLayers);
          }

          else if (state == State.attack2) {
               //Detect enemies in range of attack 2
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange2, enemyLayers);
          }

          else if (state == State.attack3){
               //Detect enemies in range of attack 3
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint3.position, attackRange3, enemyLayers);
          }

          else if (state == State.airAttack1){
               //Detect enemies in range of airAttack1
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint4.position, attackRange4, enemyLayers);
          }

          else if (state == State.airAttack2){
               //detect enemies in range of airAttack2
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint5.position, attackRange5, enemyLayers);
          }

          else if (state == State.airAttack3){
               //detect enemies in range of airAttack3 (while falling)
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint6.position, attackRange6, enemyLayers);
          }
          else {
               //detect enemies in range of airAttack3 (when hitting ground)
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint7.position, attackRange7, enemyLayers);
          }

          //turn detected enemies to face player and push back slightly
          foreach (Collider2D enemy in hitEnemies) {

               //determine the direction of enemy knockback based on player position relative to enemy
               if (enemy.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                    enemyKBDirection = 1f;
               }
               else {
                    enemyKBDirection = -1f;
               }

               // if you're doing a ground pound attack, knock the enemy back farther
               if (state == State.idle) {
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(5f * enemyKBDirection, 2f);
               }
               //otherwise, normal knockback effect
               else {
                    enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(1f * enemyKBDirection, 0f);
               }
          }

          //deal damage to the detected enemies and flashes it red
          foreach (Collider2D enemy in hitEnemies) {
               Debug.Log("We hit " + enemy.name);
               enemy.GetComponent<Sidescrolling_EnemyHealthManager>().TakeDamage(attackDamage);
               enemy.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
               StartCoroutine(FixColour(enemy));
          }
     }

     private IEnumerator FixColour(Collider2D enemy) {
          yield return new WaitForSeconds(0.1f);
          //Debug.Log("Fixing " + enemy.name + " colour");
          enemy.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
     }


     public void TakeDamage(int damage) {
          currentHealth -= damage;
          Debug.Log("Damage Taken: " + damage);
          Debug.Log("Current Health: " + currentHealth);

          //play hurt animation
          anim.SetTrigger("Hurt");

          healthBar.SetHealth(currentHealth);
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


     //this is a debug tool to see what the hitboxes of attacks look like
     void OnDrawGizmosSelected() {
          if (attackPoint1 == null) {
               return;
          }
          Gizmos.DrawWireSphere(attackPoint4.position, attackRange4);
     }

}