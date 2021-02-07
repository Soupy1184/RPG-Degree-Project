using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidescrolling_PlayerController : MonoBehaviour
{

     public Rigidbody2D rb;

    // Start is called before the first frame update
     void Start()
     {
          //test
          print("test");
     }

    // Update is called once per frame
     void Update()
     {
          if (Input.GetKey(KeyCode.A)) {
               rb.velocity = new Vector2(-5, 0);
          }
          if (Input.GetKey(KeyCode.D)) {
               rb.velocity = new Vector2(5, 0);
          }
     }
}
