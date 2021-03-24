//chris campbell - february 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupObject : MonoBehaviour {
    public PlayerController player;
	public bool playerInRange;
    public GameObject child;
    public QuestSetter questSetter;

    // Update is called once per frame
    void Update() {
        //if player is in range and interacts with object
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
        	player.Pickup(); //trigger pickup in player controller script
            StartCoroutine(DetroyCo()); //set object inactive
            if(questSetter){ //if quest, set quest
                questSetter.SetQuest();
            }
        }
    }

    IEnumerator DetroyCo(){
    	yield return new WaitForSeconds(.3f);
    	child.SetActive(false);
    }

    //check to see if the player has entered the collider
    private void OnTriggerEnter2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = true;
    	}
    }

    //check to see if the player has exited the collider
    private void OnTriggerExit2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = false;
    	}
    }

    
}
