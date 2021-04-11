//chris campbell - april 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneralLockedDoor : MonoBehaviour
{
    [Header("Door variables")]
    public BooleanValue value;
    
    public DoorType thisDoorType;
    public bool open;
    public Inventory playerInventory;
  
    public GameObject door;
    public bool playerInRange;


    public void Start(){
        open = value.initialValue;
        door.SetActive(!value.initialValue);
    }

    private void Update (){
        

        // door can be open with f key
        if(Input.GetKeyDown(KeyCode.F)){
            if(playerInRange && thisDoorType == DoorType.key){
                //checks for key
                if(playerInventory.numberOfKeys > 0){
                    // remove a player key
                    playerInventory.numberOfKeys--;

                    // open the door
                    Open();
                }
            }
        }   
    }

    // opening the door
    public void Open(){
        // set door is opened
        value.initialValue = true;
        // door will no longer have a collider
        door.SetActive(!value.initialValue);
        //enable a collider to transport player to next room
    }

    // closing the door
    // might be needed for future enhancement
    public void Close(){

    }

    private void OnTriggerEnter2D(Collider2D other){
        // player is in range if collided
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        // player is not in range if not collided
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = false;
        }
    }
}