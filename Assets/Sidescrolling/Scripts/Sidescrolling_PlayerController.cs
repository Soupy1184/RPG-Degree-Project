using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescrolling_PlayerController : MonoBehaviour
{

     private Rigidbody2D rb;
     private Animator anim;

    // Start is called before the first frame update
     private void Start()
     {
          rb = GetComponent<Rigidbody2D>();
          anim = GetComponent<Animator>();
     }

    // Update is called once per frame
     private void Update()
     {
          float hDirection = Input.GetAxis("Horizontal");

          if (hDirection < 0) {
               rb.velocity = new Vector2(-5, rb.velocity.y);
               transform.localScale = new Vector2(-1, 1);
               anim.SetBool("Running", true);
          }
          else if (hDirection > 0) {
               rb.velocity = new Vector2(5, rb.velocity.y);
               transform.localScale = new Vector2(1, 1);
               anim.SetBool("Running", true);
          }
          else {
               anim.SetBool("Running", false);
          }
          if (Input.GetKey(KeyCode.Space)) {
               rb.velocity = new Vector2(rb.velocity.x, 8);
          }
     }
}
