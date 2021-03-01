using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour {
    public PlayerController player;
	public bool playerInRange;
    public GameObject parent;
    public GameObject child;
    private bool pickedUp = false;

    // Update is called once per frame
    void Update() {
        //if player is in range and interacts with object
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
        	player.Pickup(); //trigger pickup in player controller script
            StartCoroutine(DetroyCo()); //set object inactive
            pickedUp = true;
        }
    }

    IEnumerator DetroyCo(){
    	yield return new WaitForSeconds(.3f);
    	child.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = true;
    	}
    }

    private void OnTriggerExit2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = false;
    	}
    }
}
