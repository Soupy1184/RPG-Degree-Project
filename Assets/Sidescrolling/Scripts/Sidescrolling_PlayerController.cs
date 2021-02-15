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
     private enum State { idle, running, jumping, falling, attack1, attack2, attack3 };
     private State state = State.idle;

     //Unity inspector variables
     [SerializeField] private LayerMask ground;
     [SerializeField] private float speed = 5f;
     [SerializeField] private float jumpForce = 8f;
     [SerializeField] private int money = 0;
     [SerializeField] private Text moneyCountText;

     private float attackCooldown = 0.5f;
     private float timer = 0.0f;


     //for attacking
     public Transform attackPoint1;
     public float attackRange = 0.5f;
     public LayerMask enemyLayers;


    // Start is called before the first frame update
     private void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          anim = GetComponent<Animator>();
          coll = GetComponent<Collider2D>();
     }

    // Update is called once per frame
     private void Update()
     {
          //this is used for the attack cooldown
          timer += Time.deltaTime;

          //if you are not attacking, then you can have movement animations (states)
          if (timer > attackCooldown) {
               VelocityState();
          }

          //runs every frame to manage user inputs
          Controller();

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
          if (timer <= attackCooldown) {
               //this second if represents a window between being "done" attacking and beginning another attack, to combo
               if (timer > attackCooldown - 0.1f) {
                    //transition from "attack1" to "attack2" animations
                    if (Input.GetKey("j") && state == State.attack1) {
                         //Detect enemies in range of attack 2
                         Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint1.position, attackRange, enemyLayers);

                         //deal damage to the detected enemies
                         foreach(Collider2D enemy in hitEnemies) {
                              Debug.Log("We hit " + enemy.name);
                         }
                         
                         print("Attack 2");
                         state = State.attack2;
                         timer -= 0.4f;
                    }
                    //transition from "attack2" to "attack3" animations
                    else if (Input.GetKey("j") && state == State.attack2) {
                         print("Attack 3");
                         state = State.attack3;
                         timer -= 0.4f;
                    }
                    //transition from "attack3" back to the "attack1" animation
                    else if (Input.GetKey("j") && state == State.attack3) {
                         print("Attack 1 repeat");
                         state = State.attack1;
                         timer -= 0.4f;
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
                    timer = 0;
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

     void OnDrawGizmosSelected() {
          if (attackPoint1 == null) {
               return;
          }
          Gizmos.DrawWireSphere(attackPoint1.position, attackRange);
     }

}