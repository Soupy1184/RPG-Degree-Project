using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp_Script : MonoBehaviour
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

     [SerializeField] private GameObject imp_projectile;
     [SerializeField] private float projectileSpeed;


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
               //If the player is close enough to the imp, change AI
               if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 10f) {
                    SeeingPlayerBehaviour();
               }
               else {
                    NormalBehaviour();
               }
          }
          else if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt() && attackTimer <= attackCooldown) {
               //this is so that there's an idle animation between enemy attacks (look at the animator to see how it works)
               if (attackTimer > 1f) {
                    SeeingPlayerBehaviour();
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
          //determine if the imp is above or below the standard height and go to it.
          if (rb.position.y > standardHeight + 0.5f) {
               goUpOrDown = -1;
          }
          else if (rb.position.y < standardHeight - 0.5f) {
               goUpOrDown = 1;
          }
          else {
               goUpOrDown = 0;
          }

          if (attackTimer > attackCooldown) {

               //if the imp is close enough, try to attack the player
               if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 10f && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.y - rb.position.y) < 5f) {
                    Debug.Log("Imp attacking");
                    state = State.attacking;
                    attackTimer = 0f;
                    rb.velocity = new Vector2(0, 0);

                    //face imp towards player when attacking
                    if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                         transform.localScale = new Vector2(-1, 1);
                    }
                    else if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                         transform.localScale = new Vector2(1, 1);
                    }
               }
          }

          //  else if (turnTimer <= turnTime) {
          //       turnTimer = 0;
          else {
               //if the player is to the left of the imp, move left (towards the player)
               if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                    //if the player is too close to the imp, move away from player to keep a short distance
                    if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 4) {
                         //check if sprite is facing right, and if not, face right. Don't flip if really close to the player (to prevent super fast switching)
                         if (transform.localScale.x != 1 && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) > 0.1f) {
                              transform.localScale = new Vector2(1, 1);
                         }
                         rb.velocity = new Vector2(speed, goUpOrDown * verticalSpeed);
                    }

                    else {
                         //make sure the imp doesn't leave its area
                         if (transform.position.x > leftCap) {
                              //check if sprite is facing left, and if not, face left. Don't flip if really close to the player (to prevent super fast switching)
                              if (transform.localScale.x != 1 && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) > 0.1f) {
                                   transform.localScale = new Vector2(1, 1);
                              }
                              rb.velocity = new Vector2(-speed, goUpOrDown * verticalSpeed);
                         }
                    }
               }
               //if the player is to the right of the imp, move right (towards the player)
               else if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                    //if the player is too close to the imp, move away from player to keep a short distance
                    if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 4) {
                         //check if sprite is facing left, and if not, face left. Don't flip if really close to the player (to prevent super fast switching)
                         if (transform.localScale.x != 1 && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) > 0.1f) {
                              transform.localScale = new Vector2(-1, 1);
                         }
                         rb.velocity = new Vector2(-speed, goUpOrDown * verticalSpeed);
                    }
                    else {
                         //make sure the imp doesn't leave its area
                         if (transform.position.x < rightCap) {
                              //check if sprite is facing right, and if not, face right. Don't flip if really close to player (to prevent super fast switching)
                              if (transform.localScale.x != -1 && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) > 0.1f) {
                                   transform.localScale = new Vector2(-1, 1);
                              }
                              rb.velocity = new Vector2(speed, goUpOrDown * verticalSpeed);
                         }
                    }
               }
          }
          // }
     }

     private void NormalBehaviour() {
          //determine if the imp is above or below the standard height and go to it.
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

          //if the imp got attacked, don't hit at the same time
          if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt()) {
               GameObject projectile = (GameObject)Instantiate(imp_projectile, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
               projectile.GetComponent<Sidescrolling_ProjectileScript>().SetDamage(attackDamage);

               //sends projectile in direction towards player
               Vector2 direction = player.GetComponent<Rigidbody2D>().position - this.GetComponent<Rigidbody2D>().position;
               Vector2 newvector = direction.normalized * projectileSpeed;
               projectile.GetComponent<Rigidbody2D>().velocity = newvector;

               //faces projectile towards player
               projectile.transform.right = player.transform.position - transform.position;

          }
     }

     private IEnumerator FixColour(Collider2D enemy) {
          yield return new WaitForSeconds(0.1f);
          Debug.Log("Fixing " + enemy.name + " colour");
          enemy.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
     }

}
