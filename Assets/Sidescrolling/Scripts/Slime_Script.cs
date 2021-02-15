using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Script : MonoBehaviour
{
     public Animator animator;

     public int maxHealth = 100;
     int currentHealth;

     public GameObject coin;


     private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
          currentHealth = maxHealth;
          coroutine = DieAfterTime(2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     public void TakeDamage(int damage) {
          currentHealth -= damage;

          animator.SetTrigger("Hurt");

          //play hurt animation

          if(currentHealth <= 0) {
               Die();
          }
     }

     void Die() {
          Debug.Log("Enemy died!");

          //Die animation
          animator.SetBool("Dead", true);

          //disable the enemy
          GetComponent<Rigidbody2D>().gravityScale = 0;
          GetComponent<Collider2D>().enabled = false;
          // this.enabled = false;
          //  Destroy(this.gameObject);

          //enemy drop coins
          GameObject coin1 = (GameObject)Instantiate(coin, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
          coin1.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 2);
          GameObject coin2 = (GameObject)Instantiate(coin, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
          coin1.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 2);
          GameObject coin3 = (GameObject)Instantiate(coin, new Vector2(this.GetComponent<Rigidbody2D>().position.x, this.GetComponent<Rigidbody2D>().position.y), Quaternion.identity);
          coin1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2);

          StartCoroutine(coroutine);
     }

     private IEnumerator DieAfterTime(float waitTime) {
          yield return new WaitForSeconds(1.0f);
          Debug.Log("Delete enemy now");
          Destroy(this.gameObject);

     }
}