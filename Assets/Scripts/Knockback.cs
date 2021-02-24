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
        if (other.gameObject.CompareTag("Enemy"))
        {
            // turn kinematic to dynamic, at force, and turn back to kinematic
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().currentState = EnemyState.stagger;
                //find difference between player and enemy
                Vector2 difference = enemy.transform.position - transform.position;

                //normalize difference 
                difference = difference.normalized * thrust;

                enemy.AddForce(difference, ForceMode2D.Impulse);

                // start back coroutine
                StartCoroutine(KnockCo(enemy));
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
