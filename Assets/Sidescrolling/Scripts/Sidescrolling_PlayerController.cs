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

     private int attackCooldown = 0;



     private float waitTime = 0.5f;
     private float timer = 0.0f;




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

          timer += Time.deltaTime;

          /*
          timer += Time.deltaTime;
          if (timer > waitTime - 1.5f) {
               print("0.5 seconds passed");
               timer = timer - (waitTime - 1.5f);
          }*/


          /*       if (attackCooldown > 0) {
                      attackCooldown -= 1;
                 }
                 if (attackCooldown < 10 && (state == State.attack2 || state == State.attack3)) {
                      state = State.idle;
                 }
                 if (attackCooldown == 0) {
                      VelocityState();
                 }*/


          if (timer > waitTime) {
               VelocityState();
          }


          Controller();
          anim.SetInteger("state", (int)state); //use Enumerator 'state' to set the current animation.
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

          /*
          if (attackCooldown > 0) {
               if (attackCooldown < 30 && attackCooldown >= 10) {
                    if (Input.GetKey("j") && state == State.attack1) {
                         print("Attack 2");
                         state = State.attack2;
                         attackCooldown = 80;
                    }
                    else if (Input.GetKey("j") && state == State.attack2) {
                         print("Attack 3");
                         state = State.attack3;
                         attackCooldown = 80;
                    }
                    else if (Input.GetKey("j") && state == State.attack3) {
                         print("Attack 1 repeat");
                         state = State.attack1;
                         attackCooldown = 80;
                    }
               }
          }*/

          if (timer <= waitTime) {
               if (timer > waitTime - 0.4f) {
                    if (Input.GetKey("j") && state == State.attack1) {
                         print("Attack 2");
                         state = State.attack2;
                         timer -= 0.4f;
                    }
                    else if (Input.GetKey("j") && state == State.attack2) {
                         print("Attack 3");
                         state = State.attack3;
                         timer -= 0.4f;
                    }
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


               if (Input.GetKey("j") && coll.IsTouchingLayers(ground) && attackCooldown == 0) {
                    print("Attack 1");
                    state = State.attack1;
                    timer = 0;
                    //attackCooldown = 80;
                    //rb.velocity = new Vector2(0, 0);
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
}
