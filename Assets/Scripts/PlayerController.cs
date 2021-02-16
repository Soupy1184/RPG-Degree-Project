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

    enum Direction { Up, Right, Down, Left };

	public Animator animatorUp;
	public Animator animatorDown;
	public Animator animatorLeft;
	public Animator animatorRight;

    public CharacterSetup controller;
    Animator currentAnimator;

    public VectorValue startingPosition;

    void Start() { //fires when game starts
        //moves player to the vector value object
        transform.position = startingPosition.initialValue;
        currentAnimator = animatorDown;
    }

    
    void Update() //controls player movement and animations
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
                    currentAnimator = animatorDown;
                }
                else if (movement.y > 0) //up movement
                {
                    controller.downDirection.SetActive(false);
                    controller.upDirection.SetActive(true);
                    controller.leftDirection.SetActive(false);
                    controller.rightDirection.SetActive(false);

                    animatorUp.SetFloat("Speed", movement.y);
                    currentAnimator = animatorUp;
                }
                else if (movement.x > 0) //right movement
                {
                    controller.downDirection.SetActive(false);
                    controller.upDirection.SetActive(false);
                    controller.leftDirection.SetActive(false);
                    controller.rightDirection.SetActive(true);

                    animatorRight.SetFloat("Speed", movement.x);
                    currentAnimator = animatorRight;
                }
                else if (movement.x < 0) //left movement
                {
                    controller.downDirection.SetActive(false);
                    controller.upDirection.SetActive(false);
                    controller.leftDirection.SetActive(true);
                    controller.rightDirection.SetActive(false);

                    animatorLeft.SetFloat("Speed", movement.x * -1);
                    currentAnimator = animatorLeft;
                }
            }
            else{
                animatorDown.SetFloat("Speed", movement.y);
                animatorUp.SetFloat("Speed", movement.y);
                animatorRight.SetFloat("Speed", movement.x);
                animatorLeft.SetFloat("Speed", movement.x);
            }

            // Check keys for actions and use appropiate function
            if (Input.GetKeyDown(KeyCode.Space)){  // SWING ATTACK
                currentAnimator.SetTrigger("spaceKey");
            }
        }
        else{
            currentAnimator.SetBool("isDead", true);
            //SoundManagerScript.PlaySound("death");
        }
    }

    void FixedUpdate(){ //Movement
        movement.Normalize();
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