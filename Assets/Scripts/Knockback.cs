using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // amount of force the player will give
    public float thrust;

    // to have the knockback to last
    public float knockTime;

    // Enemy's amount of damage value per attack
    public FloatValue damageVal;
    public float damage;

    private void Awake(){
        damage = damageVal.initialValue;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Breakable")){
    		other.GetComponent<DestroyObject>().ObjectDestroy();
    	}

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
                
                if (other.gameObject.CompareTag("Enemy") && other.isTrigger )
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
            }
            
        }
    }

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
