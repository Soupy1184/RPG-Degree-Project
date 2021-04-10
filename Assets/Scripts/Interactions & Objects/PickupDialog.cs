//chris campbell - february 2021
//this script manages the dialog to show when an item is picked up

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupDialog : MonoBehaviour
{
	public GameObject dialogBox;
	public Text dialogObject;
	public Text dialogText;
	public string objectText;
	public string dialog;
	public bool playerInRange;
    public GameObject child;
    public GameObject parent;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Item item;
    public BooleanValue isActive;

    void Awake(){
        //item will only ever be able to be picked up once the entire game
        parent.SetActive(isActive.initialValue);
    }

    // Update is called once per frame
    void Update(){
        //if player is in range and interacts with object
        if (Input.GetKeyDown(KeyCode.F) && playerInRange){
            if (dialogBox.activeInHierarchy){
                dialogBox.SetActive(false);
            }
            else{
                dialogBox.SetActive(true);
                dialogText.text = dialog;
                dialogObject.text = objectText;
            }
            AddItem();
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
            if (!child.activeSelf){
                this.gameObject.SetActive(false);
            }
            //set bool scriptable to false so item never appears again
            isActive.initialValue = false;
    	}
    }

    //add the item to the player inventory
    private void AddItem(){
        if(playerInventory && item){
            if(playerInventory.items.Contains(item)){
                item.numberHeld += 1;
            }
            else{
                playerInventory.items.Add(item);
                item.numberHeld += 1;
            }
        }
    }
}
