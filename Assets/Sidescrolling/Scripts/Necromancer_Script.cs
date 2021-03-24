using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer_Script : MonoBehaviour
{
     public Animator animator;

     private enum State { idle, moving, attacking };
     private State state = State.idle;

     [SerializeField] private float leftCap;
     [SerializeField] private float rightCap;
     [SerializeField] private float standardHeight;

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

     [SerializeField] private Transform teleportPoint1, teleportPoint2, teleportPoint3, teleportPoint4;
     [SerializeField] private Sidescrolling_EnemyHealthManager healthManager;
     private int currentTeleportPoint = 2, nextTeleportPoint;

     [SerializeField] private GameObject necromancerProjectile;
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

          if (healthManager.getHitCount() > 4) {
               do {
                    nextTeleportPoint = Random.Range(1, 4);
               } while (nextTeleportPoint == currentTeleportPoint);
               if (nextTeleportPoint == 1) {
                    rb.transform.position = teleportPoint1.transform.position;
                    currentTeleportPoint = 1;
               }
               else if (nextTeleportPoint == 2) {
                    rb.transform.position = teleportPoint2.transform.position;
                    currentTeleportPoint = 2;
               }
               else if (nextTeleportPoint == 3) {
                    rb.transform.position = teleportPoint3.transform.position;
                    currentTeleportPoint = 3;
               }
               else if (nextTeleportPoint == 4) {
                    rb.transform.position = teleportPoint4.transform.position;
                    currentTeleportPoint = 4;
               }
               healthManager.setHitCount(0);
          }

          // VelocityState();

          //If the enemy is currently being attacked or attacking, it won't move
          if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt() && attackTimer > attackCooldown) {


               //If the player is close enough to the necromancer, change AI
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
                    if (turnTimer > turnTime) {
                         SeeingPlayerBehaviour();
                         turnTimer = 0f;
                    }
                    else {
                         rb.velocity = new Vector2(rb.velocity.x, 0f);
                    }
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

          if (attackTimer > attackCooldown) {

               //if the necromancer is close enough, try to attack the player
               if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 8f && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.y - rb.position.y) < 5f) {
                    Debug.Log("Imp attacking");
                    state = State.attacking;
                    attackTimer = 0f;
                    rb.velocity = new Vector2(0, 0);

                    //face enemy towards player when attacking
                    if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                         transform.localScale = new Vector2(-1, 1);
                    }
                    else if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                         transform.localScale = new Vector2(1, 1);
                    }
               }
          }
     }

     private void NormalBehaviour() {
          state = State.idle;
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
               GameObject projectile = (GameObject)Instantiate(necromancerProjectile, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
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
