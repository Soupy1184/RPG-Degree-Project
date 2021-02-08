using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescrolling_PlayerController : MonoBehaviour
{
     //Start() variables
     private Rigidbody2D rb;
     private Animator anim;
     private Collider2D coll;

     //Finite State Machine (FSM) for animations
     private enum State { idle, running, jumping, falling }
     private State state = State.idle;

     //Unity inspector variables
     [SerializeField] private LayerMask ground;
     [SerializeField] private float speed = 5f;
     [SerializeField] private float jumpForce = 8f;


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
          Movement();
          VelocityState();
          anim.SetInteger("state", (int)state); //use Enumerator 'state' to set the current animation.
     }


     private void Movement() {
          float hDirection = Input.GetAxis("Horizontal");

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
     }

     private void VelocityState() {
          if(state == State.jumping) {
               if (rb.velocity.y < .1f) {
                    state = State.falling;
                    print("Falling");
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
