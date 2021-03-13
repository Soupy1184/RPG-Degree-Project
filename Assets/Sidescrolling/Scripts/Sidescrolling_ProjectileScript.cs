using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescrolling_ProjectileScript : MonoBehaviour
{
     [SerializeField] private GameObject player;
     private int projectileDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void SetDamage(int damage) {
          projectileDamage = damage;
     }


     void OnTriggerEnter2D(Collider2D coll) {

          if (coll.CompareTag("Player")) {
               coll.GetComponent<Sidescrolling_PlayerController>().TakeDamage(projectileDamage);
               coll.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);
               StartCoroutine(FixColour(coll));

               Destroy(this.gameObject);
               this.gameObject.SetActive(false);
               Debug.Log("Projectile hit player");
          }

     }

     private IEnumerator FixColour(Collider2D enemy) {
          yield return new WaitForSeconds(0.1f);
          Debug.Log("Fixing " + enemy.name + " colour");
          enemy.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
     }

}
