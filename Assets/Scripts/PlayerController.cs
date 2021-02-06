using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D rb; 
	public float moveSpeed = 2f;
	Vector2 movement;

	public Animator animatorUp;
	public Animator animatorDown;
	public Animator animatorLeft;
	public Animator animatorRight;

    // Update is called once per frame
    void Update()
    {
    	//Input
    	movement.x = Input.GetAxisRaw("Horizontal");
    	movement.y = Input.GetAxisRaw("Vertical");

    	
    }

    void FixedUpdate(){
    	//Movement
    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
    



    		//Move(); 
	      	//animator.SetFloat("Speed", Mathf.Abs(moveBy));
      		
    	//}
    	// if (isDead == true){
     //  		animator.SetBool("isDead", true);
     //        //SoundManagerScript.PlaySound("death");
     //  	}

// void Move() { 
// 	    //moveBy = Input.GetAxisRaw("Horizontal") * speed;  
	    
// 	    rb.velocity = new Vector2(moveBy, rb.velocity.y); 
// 	    if(Input.GetKeyDown (KeyCode.RightArrow)){
// 	    	animatorRight.SetFloat("Speed", Mathf.Abs(moveBy));
// 	    }
	    
// 	}



	// void OnCollisionEnter2D (Collision2D collision)
 //    {
	//    	if(collision.transform.tag == "enemy")
 //        {
 //              isDead = true;
 //        }

 //    }

    // void OnTriggerEnter2D(Collider2D other){
    // 	if(other.gameObject.CompareTag("coins")){
    // 		Destroy(other.gameObject);
    // 	}
    // 	if(other.gameObject.CompareTag("chest")){
    // 		SceneManager.LoadScene("BetweenLevel");
    // 	}
    // 	if(other.gameObject.CompareTag("chest2")){
    // 		SceneManager.LoadScene("GameOver");
    // 		PersistentManagerScript.Instance.Value = 0;
    // 	}
    // 	// if(other.gameObject.CompareTag("enemy")){
    // 	// 	isDead = true;
    // 	// }
    // }

