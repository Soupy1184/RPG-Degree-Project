//Zachary Moorman

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescrolling_EnemyHealthManager : MonoBehaviour
{
     public Animator animator;

     public int maxHealth = 100;
     private int currentHealth;

     public Enemy_HealthbarBehaviour healthbar;

     private bool isHurt;
     private bool isDead;

     public GameObject itemDrop;
     public int dropCountTotal;

     private IEnumerator coroutine;

     private int hitCount = 0;

     private enum EnemyType { slime, reaper, imp, executioner, necromancer }
     [SerializeField] private EnemyType enemyType;
     //For sound effects
     private GameObject hurt, die;


     // Start is called before the first frame update
     void Start() {
          //find the appropriate sound effects depending on the enemy type
          if (enemyType == EnemyType.slime) {
               hurt = GameObject.Find("slimeHurt");
               die = GameObject.Find("slimeDie");
          }
          else if (enemyType == EnemyType.reaper) {
               hurt = GameObject.Find("reaperHurt");
               die = GameObject.Find("reaperDie");
          }
          else if (enemyType == EnemyType.imp) {
               hurt = GameObject.Find("impHurt");
               die = GameObject.Find("impDie");
          }
          else if (enemyType == EnemyType.executioner) {
               hurt = GameObject.Find("executionerHurt");
               die = GameObject.Find("executionerDie");
          }
          else if (enemyType == EnemyType.necromancer) {
               hurt = GameObject.Find("necromancerHurt");
               die = GameObject.Find("necromancerDie");
          }
          currentHealth = maxHealth;
          coroutine = DieAfterTime(1.0f);
          healthbar.SetHealth(currentHealth, maxHealth);
     }

     // Update is called once per frame
     void Update() {

     }


     public void TakeDamage(int damage) {
          //this counter is mainly for the necromancer boss
          hitCount++;

          //for playing sounds
          hurt.GetComponent<AudioSource>().Play();

          bool isCriticalHit = Random.Range(0, 100) < 30;
          if (isCriticalHit) {
               damage = (int)(damage * 1.53f);
          }
          Sidescrolling_DamagePopup.Create(this.transform.position, damage, isCriticalHit);

          currentHealth -= damage;

          healthbar.SetHealth(currentHealth, maxHealth);

          //play hurt animation
          animator.SetTrigger("Hurt");

          //this is used for other scripts to know if the enemy is currently being hurt
          isHurt = true;
          if (isDead) {
               this.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
          }

          if (currentHealth <= 0) {
               Die();
          }
          else {
               StartCoroutine(HurtDelay(0.5f));
          }
     }

     public bool IsHurt() {
          return isHurt;
     }

     public int getHitCount() {
          return hitCount;
     }
     public void setHitCount(int count) {
          hitCount = count;
     }

     void Die() {
          Debug.Log("Enemy died!");

          //for sounds
          die.GetComponent<AudioSource>().Play();

          //Die animation
          animator.SetBool("Dead", true);

          //disable the enemy
          GetComponent<Rigidbody2D>().gravityScale = 0;
          GetComponent<Collider2D>().enabled = false;
          this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

          //spawn the dropped items when the enemy dies, and determine its starting velocity.
          GameObject drop = (GameObject)Instantiate(itemDrop, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
          drop.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 2.0f);
          
          int dropCount = 1;
          float dropXVelocity = 1.0f;
          float dropXDirection;

          if (dropCount < dropCountTotal) {
               do {
                    if (dropCount % 2 == 0) {
                         dropXDirection = -1.0f;
                    }
                    else {
                         dropXDirection = 1.0f;
                         if (dropCount >= 3) {
                              dropXVelocity++;
                         }
                    }

                    drop = (GameObject)Instantiate(itemDrop, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
                    drop.GetComponent<Rigidbody2D>().velocity = new Vector2(dropXVelocity * dropXDirection, 2.0f);

                    dropCount++;
               } while (dropCount < dropCountTotal);
          }
          StartCoroutine(coroutine);
     }

     private IEnumerator HurtDelay(float waitTime) {
          yield return new WaitForSeconds(waitTime);
          isHurt = false;
     }

     private IEnumerator DieAfterTime(float waitTime) {
          yield return new WaitForSeconds(waitTime);
          Debug.Log("Delete enemy now");
          Destroy(this.gameObject);

     }
}
