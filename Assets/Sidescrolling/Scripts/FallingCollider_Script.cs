using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCollider_Script : MonoBehaviour
{

     public Transform teleportPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other) {
          if (other.CompareTag("Player")) {
               other.GetComponent<Rigidbody2D>().position = teleportPosition.position;
               other.GetComponent<Sidescrolling_PlayerController>().TakeDamage(20);
          }

     }
}
