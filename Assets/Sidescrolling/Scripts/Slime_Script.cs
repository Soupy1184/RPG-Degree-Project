using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Script : MonoBehaviour
{
     public Animator animator;

     [SerializeField] private float leftCap;
     [SerializeField] private float rightCap;

     [SerializeField] private float speed;
     [SerializeField] private LayerMask ground;
     private Collider2D coll;
     private Rigidbody2D rb;

     [SerializeField] private float turnTime;
     private float turnTimer;

     private bool facingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
          coll = GetComponent<Collider2D>();
          rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
          turnTimer += Time.deltaTime;

          if (!this.GetComponent<Sidescrolling_EnemyHealthManager>().IsHurt()) {
               NormalMovement();
          }
    }

     private void NormalMovement() {
          if (facingLeft) {
               if (transform.position.x > leftCap) {
                    //check if sprite is facing right, and if not, face right.
                    if (transform.localScale.x != 1) {
                         transform.localScale = new Vector2(1, 1);
                    }

                    if (coll.IsTouchingLayers(ground)) {
                         rb.velocity = new Vector2(-speed, rb.velocity.y);
                    }
               }
               else {
                    facingLeft = false;
               }
          }
          else {
               if (transform.position.x < rightCap) {
                    //check if sprite is facing right, and if not, face right.
                    if (transform.localScale.x != -1) {
                         transform.localScale = new Vector2(-1, 1);
                    }

                    if (coll.IsTouchingLayers(ground)) {
                         rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
               }
               else {
                    facingLeft = true;
               }
          }
     }
}
