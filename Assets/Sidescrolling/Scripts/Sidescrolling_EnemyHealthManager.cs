using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescrolling_EnemyHealthManager : MonoBehaviour
{
     public Animator animator;

     public int maxHealth = 100;
     private int currentHealth;

     private bool isHurt;

     public GameObject coin;


     private IEnumerator coroutine;

     // Start is called before the first frame update
     void Start() {
          currentHealth = maxHealth;
          coroutine = DieAfterTime(1.0f);
     }

     // Update is called once per frame
     void Update() {

     }


     public void TakeDamage(int damage) {
          currentHealth -= damage;

          //play hurt animation
          animator.SetTrigger("Hurt");

          //this is used for other scripts to know if the slime is currently being hurt
          isHurt = true;

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

     void Die() {
          Debug.Log("Enemy died!");

          //Die animation
          animator.SetBool("Dead", true);

          //disable the enemy
          GetComponent<Rigidbody2D>().gravityScale = 0;
          GetComponent<Collider2D>().enabled = false;
          this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
          // this.enabled = false;
          //  Destroy(this.gameObject);

          //enemy drop coins
          GameObject coin1 = (GameObject)Instantiate(coin, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
          coin1.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 2.0f);
          GameObject coin2 = (GameObject)Instantiate(coin, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
          coin2.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 2.0f);
          GameObject coin3 = (GameObject)Instantiate(coin, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
          coin3.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 2.0f);

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
