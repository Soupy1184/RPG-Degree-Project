using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D rb; 
	public float moveSpeed;
    public bool isDead;
	Vector2 movement;

	public Animator animatorUp;
	public Animator animatorDown;
	public Animator animatorLeft;
	public Animator animatorRight;

    public CharacterSetup controller;

    public VectorValue startingPosition;

    void Start() {
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
    	//Input
    	movement.x = Input.GetAxisRaw("Horizontal");
    	movement.y = Input.GetAxisRaw("Vertical");
        if (!isDead){
            if (movement.x != 0 || movement.y != 0)
            {
                if (movement.y < 0) //down movement
                {
                    controller.downDirection.SetActive(true);
                    controller.upDirection.SetActive(false);
                    controller.leftDirection.SetActive(false);
                    controller.rightDirection.SetActive(false);

                    animatorDown.SetFloat("Speed", movement.y);
                }
                else if (movement.y > 0) //up movement
                {
                    controller.downDirection.SetActive(false);
                    controller.upDirection.SetActive(true);
                    controller.leftDirection.SetActive(false);
                    controller.rightDirection.SetActive(false);

                    animatorUp.SetFloat("Speed", movement.y);
                }
                else if (movement.x > 0) //right movement
                {
                    controller.downDirection.SetActive(false);
                    controller.upDirection.SetActive(false);
                    controller.leftDirection.SetActive(false);
                    controller.rightDirection.SetActive(true);

                    animatorRight.SetFloat("Speed", movement.x);
                }
                else if (movement.x < 0) //left movement
                {
                    controller.downDirection.SetActive(false);
                    controller.upDirection.SetActive(false);
                    controller.leftDirection.SetActive(true);
                    controller.rightDirection.SetActive(false);

                    animatorLeft.SetFloat("Speed", movement.x * -1);
                }
            }
            else{
                animatorDown.SetFloat("Speed", movement.y);
                animatorUp.SetFloat("Speed", movement.y);
                animatorRight.SetFloat("Speed", movement.x);
                animatorLeft.SetFloat("Speed", movement.x);
            }

            // Check keys for actions and use appropiate function
            //
            if (Input.GetKey(KeyCode.Space)){  // SWING ATTACK
                animatorDown.SetTrigger("spaceKey");
                animatorUp.SetTrigger("spaceKey");
                animatorRight.SetTrigger("spaceKey");
                animatorLeft.SetTrigger("spaceKey");
            }
        }
        else{
            animatorDown.SetBool("isDead", true);
            animatorUp.SetBool("isDead", true);
            animatorRight.SetBool("isDead", true);
            animatorLeft.SetBool("isDead", true);
            //SoundManagerScript.PlaySound("death");
        }
    }

    void FixedUpdate(){ //Movement
    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D (Collision2D collision){

    }

    void OnTriggerEnter2D(Collider2D other){
    	
    }
}

// if(other.gameObject.CompareTag("coins")){
    	// 	Destroy(other.gameObject);
    	// }
    	// if(other.gameObject.CompareTag("chest")){
    	// 	SceneManager.LoadScene("BetweenLevel");
    	// }
    	// if(other.gameObject.CompareTag("chest2")){
    	// 	SceneManager.LoadScene("GameOver");
    	// 	PersistentManagerScript.Instance.Value = 0;
    	// }




  

