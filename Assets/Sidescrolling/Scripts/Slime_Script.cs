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


     public Transform attackPoint;
     public float attackRange;
     public LayerMask playerLayer;
     public int attackDamage;

     //For sound effects
     private GameObject attack;

     // Start is called before the first frame update
     void Start()
    {
          attack = GameObject.Find("slimeAttack");
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
               if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 5f && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.y - rb.position.y) < 1f) {
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
               rb.velocity = new Vector2(0, rb.velocity.y);

               //face slime towards player when attacking
               if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                    transform.localScale = new Vector2(-1, 1);
               }
               else if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                    transform.localScale = new Vector2(1, 1);
               }

               //StartCoroutine(AttackEnum(0.3f));
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

     private IEnumerator AttackEnum(float waitTime) {
          yield return new WaitForSeconds(waitTime);

          attack.GetComponent<AudioSource>().Play();

          bool alreadyDamaged = false;
          Collider2D[] hitEnemies;


          //Detect enemies in range of attack
          hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

          //deal damage to the detected enemies and flashes it red
          foreach (Collider2D enemy in hitEnemies) {
               if (!enemy.GetComponent<Sidescrolling_PlayerController>().isDodging() && alreadyDamaged == false) {
                    alreadyDamaged = true;

                    Debug.Log("Executioner hit " + enemy.name);
                    enemy.GetComponent<Sidescrolling_PlayerController>().TakeDamage(attackDamage);
                    enemy.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    StartCoroutine(FixColour(enemy));

                    //turn detected enemies to face player and push back slightly
                    if (enemy.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                         enemy.transform.localScale = new Vector2(-1, 1);
                         enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, 0f);
                    }
                    else if (enemy.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                         enemy.transform.localScale = new Vector2(1, 1);
                         enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 0f);
                    }
               }
          }
     }

     private IEnumerator FixColour(Collider2D enemy) {
          yield return new WaitForSeconds(0.1f);
          Debug.Log("Fixing " + enemy.name + " colour");
          enemy.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
     }

     void OnDrawGizmosSelected() {
          if (attackPoint == null) {
               return;
          }
          Gizmos.DrawWireSphere(attackPoint.position, attackRange);
     }

}
