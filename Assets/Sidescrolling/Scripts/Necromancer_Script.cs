using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer_Script : MonoBehaviour
{
     public Animator animator;

     private enum State { idle, moving, attacking, summoning };
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
     [SerializeField] private GameObject teleportAnimation;

     [SerializeField] private GameObject necromancerProjectile;
     [SerializeField] private float projectileSpeed;

     //this is used to make the necromancer summon after every few attacks
     private int attackCount = 0;
     [SerializeField] private GameObject reaper;
     [SerializeField] private GameObject imp;


     //For sound effects
     private GameObject fireballSound, teleportSound, summonSound;


     // Start is called before the first frame update
     void Start() {
          fireballSound = GameObject.Find("fireball");
          teleportSound = GameObject.Find("necromancerTeleport");
          summonSound = GameObject.Find("necromancerSummon");
          coll = GetComponent<Collider2D>();
          rb = GetComponent<Rigidbody2D>();
     }

     // Update is called once per frame
     void Update() {
          turnTimer += Time.deltaTime;
          attackTimer += Time.deltaTime;


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

          //this is used to check if the necromancer should teleport to another area in the boss room and if so, it does
          if (animator.GetBool("Dead") == false)
               TeleportCheck();

          //use Enumerator 'state' to set the current animation.
          animator.SetInteger("state", (int)state);


     }

     private void TeleportCheck() {
          if (healthManager.getHitCount() > 4) {
               teleportSound.GetComponent<AudioSource>().Play();

               rb.velocity = new Vector2(0, 0);
               Instantiate(teleportAnimation, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);

               do {
                    nextTeleportPoint = Random.Range(1, 5);
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

               StartCoroutine(SecondTeleportEffect());
          }
     }

     private void SeeingPlayerBehaviour() {

          if (attackTimer > attackCooldown) {

               //if the necromancer is close enough, try to attack the player
               if (Mathf.Abs(player.GetComponent<Rigidbody2D>().position.x - rb.position.x) < 8f && Mathf.Abs(player.GetComponent<Rigidbody2D>().position.y - rb.position.y) < 5f) {
                    attackTimer = 0f;
                    rb.velocity = new Vector2(0, 0);
                    //face enemy towards player
                    if (player.GetComponent<Rigidbody2D>().position.x > rb.position.x) {
                         transform.localScale = new Vector2(-1, 1);
                    }
                    else if (player.GetComponent<Rigidbody2D>().position.x < rb.position.x) {
                         transform.localScale = new Vector2(1, 1);
                    }

                    if (attackCount < 2) {
                         Debug.Log("Necromancer attacking");
                         state = State.attacking;
                         attackCount++;
                    }
                    else {
                         Debug.Log("Necromancer summoning");
                         state = State.summoning;
                         attackCount = 0;
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

     private void Summon() {
          summonSound.GetComponent<AudioSource>().Play();
          int randomEnemyChosen = Random.Range(1, 10);
          if (randomEnemyChosen < 4) {
               GameObject reaperSummon = (GameObject)Instantiate(reaper, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
               reaperSummon.GetComponent<Reaper_Script>().SetLeftAndRightCapAndHeight(leftCap, rightCap, standardHeight);
          }
          else if (randomEnemyChosen < 10) {
               GameObject impSummon = (GameObject)Instantiate(imp, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
               impSummon.GetComponent<Imp_Script>().SetLeftAndRightCapAndHeight(leftCap, rightCap, standardHeight);
          }
          
          //coin1.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 2.0f);
     }

     private IEnumerator AttackEnum(float waitTime) {
          yield return new WaitForSeconds(waitTime);

          //if the imp got attacked, don't hit at the same time
          if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt()) {
               fireballSound.GetComponent<AudioSource>().Play();

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

     private IEnumerator SecondTeleportEffect() {
          yield return new WaitForSeconds(0.1f);
          Instantiate(teleportAnimation, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
     }

     private IEnumerator FixColour(Collider2D enemy) {
          yield return new WaitForSeconds(0.1f);
          Debug.Log("Fixing " + enemy.name + " colour");
          enemy.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
     }

}
