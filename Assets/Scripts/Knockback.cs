using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // amount of force the player will give
    public float thrust;
    // to have the knockback to last
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Breakable")){
    		other.GetComponent<DestroyObject>().ObjectDestroy();
    	}

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") )
        {
            // turn kinematic to dynamic, at force, and turn back to kinematic
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }

                //find difference between player and enemy
                Vector2 difference = hit.transform.position - transform.position;

                //normalize difference 
                difference = difference.normalized * thrust;

                hit.AddForce(difference, ForceMode2D.Impulse);

                // start back coroutine
                StartCoroutine(KnockCo(hit));
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
