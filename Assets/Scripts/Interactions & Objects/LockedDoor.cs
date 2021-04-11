/*
    written by: Afieqha Mieza
    date: March 2021
    function: script for loocked doors in Level 1
*/




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for now the door will only recquire keys
// if there is extra time to add, 
// could add more doors with different opening methods
// like enemy and button
public enum DoorType
{
    key,
    enemy, 
    button
}

public class LockedDoor : MonoBehaviour
{
    [Header("Door variables")]
    public BooleanValue value;
    public BooleanValue room5Transition;
    public DoorType thisDoorType;
    public bool open;
    public Inventory playerInventory;
    public BoxCollider2D toRoom5;
    public GameObject door;
    public bool playerInRange;

    [Header("Change Dialog")]
    public GameObject dialogChange_GO;
    public Dialog dialogChange;

    public void Start(){
        open = value.initialValue;
        toRoom5.enabled = false;
        door.SetActive(!value.initialValue);
        toRoom5.enabled = room5Transition.initialValue; 
        dialogChange = dialogChange_GO.GetComponent<Dialog>();
    }

    private void Update (){
        // set dialogs based on presence of key
        if(playerInventory.numberOfKeys > 0){
            dialogChange.dialog = "You found the key! Use it to open the door.";
        }else{
            dialogChange.dialog = "Uh Oh, you will need a key to unlock the door! It should be somewhere in other rooms. Happy searching!";
        }

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
        room5Transition.initialValue = true;
        // door will no longer have a collider
        door.SetActive(!value.initialValue);
        //enable a collider to transport player to next room
        toRoom5.enabled = room5Transition.initialValue; 
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