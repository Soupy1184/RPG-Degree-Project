using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D rb; 
	public float speed;
	public Animator animator;
	public bool isDead = false;
	float moveBy;
	public string facing = "Up";
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!PauseMenuBehaviour.isPaused && !isDead){

    		Move(); 
	      	animator.SetFloat("Speed", Mathf.Abs(moveBy));
      		
    	//}
    	// if (isDead == true){
     //  		animator.SetBool("isDead", true);
     //        //SoundManagerScript.PlaySound("death");
     //  	}
    }

    void Move() { 
	    float x = Input.GetAxisRaw("Horizontal"); 
	    moveBy = x * speed; 
	    rb.velocity = new Vector2(moveBy, rb.velocity.y); 
	    // if(Input.GetKeyDown (KeyCode.RightArrow)){
	    // 	transform.Rotate(0, 180, 0);
	    // 	facing = "Right";
	    // }
	    // if(Input.GetKeyDown (KeyCode.LeftArrow) && !facing){
	    // 	transform.Rotate(0, 180, 0);
	    // 	facing = true;
	    // }
	    
	}

	void OnCollisionEnter2D (Collision2D collision)
    {
	   	if(collision.transform.tag == "enemy")
        {
              isDead = true;
        }

    }

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
}

