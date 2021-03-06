//chris campbell - february 2021
//resrouce: https://www.youtube.com/watch?v=1NCvpZDtTMI&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=12
//this script manages the dialog for objects in game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
	public GameObject dialogBox;
	public Text dialogObject;
	public Text dialogText;
	public string objectText;
	public string dialog;
	public bool playerInRange;
	public bool automatic;

    // Update is called once per frame
    void Update() {
		//if player is in range and interacts with the object
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
        	if(dialogBox.activeInHierarchy){
        		dialogBox.SetActive(false);
        	}
        	else{
        		dialogBox.SetActive(true);
        		dialogText.text = dialog;
        		dialogObject.text = objectText;
        	}
        }
    }

	//when player enters collider of object
    private void OnTriggerEnter2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = true;
			if (automatic){
				DisplayDialog();
			}
    	}
    }

	//when player leaves collider of object
    private void OnTriggerExit2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = false;
    		dialogBox.SetActive(false);
			if (automatic){
				Destroy(this.gameObject);
			}
    	}
    }

	//show dialog UI
	private void DisplayDialog(){
		dialogBox.SetActive(true);
        dialogText.text = dialog;
    	dialogObject.text = objectText;
	}
}
