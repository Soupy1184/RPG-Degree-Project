using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper_Script : MonoBehaviour
{
     public Animator animator;

     private enum State { idle, moving, attacking };
     private State state = State.idle;

     [SerializeField] private float leftCap;
     [SerializeField] private float rightCap;
     [SerializeField] private float standardHeight;

     [SerializeField] private float speed;
     [SerializeField] private float verticalSpeed;
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

     private int goUpOrDown;


     // Start is called before the first frame update
     void Start() {
          coll = GetComponent<Collider2D>();
          rb = GetComponent<Rigidbody2D>();
     }

     // Update is called once per frame
     void Update() {
          turnTimer += Time.deltaTime;
          attackTimer += Time.deltaTime;

          //If the enemy is currently being attacked or attacking, it won't move
          if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt() && attackTimer > attackCooldown) {

               VelocityState();
               //If the player is close enough to the slime, change AI
               if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 5f) {
                    SeeingPlayerBehaviour();
               }
               else {
                    NormalBehaviour();
               }
          }
          else if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt() && attackTimer <= attackCooldown) {
               //this is so that there's an idle animation between enemy attacks (look at the animator to see how it works)
               if (attackTimer > 1f) {
                    NormalBehaviour();
               }
               else {
                    state = State.idle;
                    rb.velocity = new Vector2(0, 0);
               }
          }

          //use Enumerator 'state' to set the current animation.
          animator.SetInteger("state", (int)state);
     }

     private void SeeingPlayerBehaviour() {
          //Determine if the enemy is above or below the player (slightly above)
          if (player.GetComponent<Rigidbody2D>().position.y + 1f >= rb.position.y) {
               //player above enemy
               goUpOrDown = 1;
          }
          else if (player.GetComponent<Rigidbody2D>().position.y + 1f < rb.position.y) {
               goUpOrDown = -1;
          }

          //if the reaper is close enough, try to attack the player
          if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 1f && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.y - rb.position.y) < 1.5f) {
               Debug.Log("Enemy attacking");
               state = State.attacking;
               attackTimer = 0f;
               rb.velocity = new Vector2(0, rb.velocity.y);

               //face reaper towards player when attacking
               if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                    transform.localScale = new Vector2(-1, 1);
               }
               else if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                    transform.localScale = new Vector2(1, 1);
               }
          }

        //  else if (turnTimer <= turnTime) {
        //       turnTimer = 0;
               //if the player is to the left of the reaper, move left (towards the player)
               if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                    //make sure the reaper doesn't leave its area
                    if (transform.position.x > leftCap) {
                         //check if sprite is facing right, and if not, face right. Don't flip if really close to the player (to prevent super fast switching)
                         if (transform.localScale.x != 1 && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) > 0.1f) {
                              transform.localScale = new Vector2(1, 1);
                         }
                         rb.velocity = new Vector2(-speed, goUpOrDown * verticalSpeed);
                    }
               }
               //if the player is to the right of the reaper, move right (towards the player)
               else if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                    //make sure the reaper doesn't leave its area
                    if (transform.position.x < rightCap) {
                         //check if sprite is facing right, and if not, face right. Don't flip if really close to player (to prevent super fast switching)
                         if (transform.localScale.x != -1 && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) > 0.1f) {
                              transform.localScale = new Vector2(-1, 1);
                         }
                         rb.velocity = new Vector2(speed, goUpOrDown * verticalSpeed);
                    }
               }
         // }
     }

     private void NormalBehaviour() {
          //determine if the reaper is above or below the standard height and go to it.
          if (rb.position.y > standardHeight + 0.5f) {
               goUpOrDown = -1;
          }
          else if (rb.position.y < standardHeight - 0.5f) {
               goUpOrDown = 1;
          }
          else {
               goUpOrDown = 0;
          }

          if (facingLeft) {
               if (transform.position.x > leftCap) {
                    //check if sprite is facing left, and if not, face left.
                    if (transform.localScale.x != 1) {
                         transform.localScale = new Vector2(1, 1);
                    }
                    rb.velocity = new Vector2(-speed, goUpOrDown * verticalSpeed);
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
                    rb.velocity = new Vector2(speed, goUpOrDown * verticalSpeed);
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

          Collider2D[] hitEnemies;

          //if the slime got attacked, don't hit at the same time
          if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt()) {
               //Detect enemies in range of attack
               hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

               //deal damage to the detected enemies and flashes it red
               foreach (Collider2D enemy in hitEnemies) {
                    Debug.Log("Reaper hit " + enemy.name);
                    enemy.GetComponent<Sidescrolling_PlayerController>().TakeDamage(attackDamage);
                    enemy.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
                    StartCoroutine(FixColour(enemy));
               }

               //turn targets to face attacker and push back slightly
               foreach (Collider2D enemy in hitEnemies) {
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
