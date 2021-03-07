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

     }

     private void NormalBehaviour() {
          
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
