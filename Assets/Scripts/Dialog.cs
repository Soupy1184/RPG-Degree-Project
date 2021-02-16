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

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
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

    private void OnTriggerEnter2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = true;
    	}
    }

    private void OnTriggerExit2D(Collider2D other){
    	if (other.CompareTag("Player")){
    		playerInRange = false;
    		dialogBox.SetActive(false);
    	}
    }
}
