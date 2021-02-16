using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Script : MonoBehaviour
{
     public Animator animator;

     private enum State { idle, moving, attacking };
     private State state = State.idle;

     [SerializeField] private float leftCap;
     [SerializeField] private float rightCap;

     [SerializeField] private float speed;
     [SerializeField] private LayerMask ground;
     private Collider2D coll;
     private Rigidbody2D rb;

     [SerializeField] private GameObject player;

     [SerializeField] private float turnTime;
     private float turnTimer;

     [SerializeField] private float attackCooldown;
     private float attackTimer;

     private bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
          coll = GetComponent<Collider2D>();
          rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
          turnTimer += Time.deltaTime;
          attackTimer += Time.deltaTime;

          //If the enemy is currently being attacked or attacking, it won't move
          if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt() && attackTimer > attackCooldown) {
               //If the player is close enough to the slime, change AI

               VelocityState();
               if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 5f) {
                    SeeingPlayerBehaviour();
               }
               else {
                    NormalBehaviour();
               }
          }

          //use Enumerator 'state' to set the current animation.
          animator.SetInteger("state", (int)state);
     }

     private void SeeingPlayerBehaviour() {

          //if the slime is close enough, try to attack the player
          if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 1f) {
               Debug.Log("Enemy attacking");
               state = State.attacking;
               attackTimer = 0f;
          }

          //if the player is to the left of the slime, move left (towards the player)
          else if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
               //make sure the slime doesn't fall off its area
               if (transform.position.x > leftCap) {
                    //check if sprite is facing right, and if not, face right.
                    if (transform.localScale.x != 1) {
                         transform.localScale = new Vector2(1, 1);
                    }
                    //make sure the slime is on the ground
                    if (coll.IsTouchingLayers(ground)) {
                         rb.velocity = new Vector2(-speed, rb.velocity.y);
                    }
               }
               
          }
          //if the player is to the right of the slime, move right (towards the player)
          else if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
               //make sure the slime doesn't fall off its area
               if (transform.position.x < rightCap) {
                    //check if sprite is facing right, and if not, face right.
                    if (transform.localScale.x != -1) {
                         transform.localScale = new Vector2(-1, 1);
                    }
                    //make sure the slime is on the ground
                    if (coll.IsTouchingLayers(ground)) {
                         rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
               }
               
          }
     }

     private void NormalBehaviour() {
          if (facingLeft) {
               if (transform.position.x > leftCap) {
                    //check if sprite is facing right, and if not, face right.
                    if (transform.localScale.x != 1) {
                         transform.localScale = new Vector2(1, 1);
                    }
                    //make sure the slime is on the ground
                    if (coll.IsTouchingLayers(ground)) {
                         rb.velocity = new Vector2(-speed, rb.velocity.y);
                    }
               }
               else {
                    facingLeft = false;
               }
          }
          else {
               if (transform.position.x < rightCap) {
                    //check if sprite is facing right, and if not, face right.
                    if (transform.localScale.x != -1) {
                         transform.localScale = new Vector2(-1, 1);
                    }
                    //make sure the slime is on the ground
                    if (coll.IsTouchingLayers(ground)) {
                         rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
               }
               else {
                    facingLeft = true;
               }
          }
     }

     private void VelocityState() {
          if (Mathf.Abs(rb.velocity.x) > 2f) {
               state = State.moving;
          }
          else {
               state = State.idle;
          }
     }
}
