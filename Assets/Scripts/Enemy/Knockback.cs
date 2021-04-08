using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("Knockback")]
    public float thrust;
    public float knockTime;

    [Header("Enemy")]
    public FloatValue damageVal;
    public float damage;

    // take initiali value of health when awake
    private void Awake(){
        damage = damageVal.initialValue;
    }

    private void OnTriggerEnter2D(Collider2D other){
        // collision with breakable objects
        // simply destroy objects
        if(other.gameObject.CompareTag("Breakable")){
    		other.GetComponent<DestroyObject>().ObjectDestroy();
    	}

        // collision between enemy and player
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") )
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();

            if (hit != null)
            {
                //find difference between player and enemy
                Vector2 difference = hit.transform.position - transform.position;

                //normalize difference 
                difference = difference.normalized * thrust;

                // adding force to the enemy to cause knockback
                hit.AddForce(difference, ForceMode2D.Impulse);
                
                // knockback enemy if it is attacked by player
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger )
                {
                    // set state
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;

                    // call coroutine
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
            }
            
        }
    }

    // coroutine to knockback whenever player strikes
    // makes knocked enemies knockback a little
    private IEnumerator KnockCo(Rigidbody2D enemy){
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);

            // velocity to 0
            enemy.velocity = Vector2.zero;

            //reset state
            enemy.GetComponent<Enemy>().currentState = EnemyState.idle;
        }
    }
}
