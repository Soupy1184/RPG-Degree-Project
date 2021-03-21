/* 
    This script is for loocked doors in Level 1
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
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public BoxCollider2D toRoom5;
    public GameObject door;
    public bool playerInRange;

    [Header("Change Dialog")]
    public GameObject dialogChange_GO;
    public Dialog dialogChange;

    public void Start(){
        toRoom5.enabled = false;
        // playerInventory.numberOfKeys++;
        dialogChange = dialogChange_GO.GetComponent<Dialog>();
    }
    private void Update (){

        if(playerInventory.numberOfKeys > 0){
            dialogChange.dialog = "You found the key! Use it to open the door.";
        }else{
            dialogChange.dialog = "Uh Oh, you will need a key to unlock the door! It should be somewhere in other rooms. Happy searching!";
        }

        // door can be open with space bar
        if(Input.GetKeyDown(KeyCode.F)){
            if(playerInRange && thisDoorType == DoorType.key){
                //checks for key
                if(playerInventory.numberOfKeys > 0){
                    // remove a player key
                    // dialogChange.dialog = "You found the key! Use it to open the door.";
                    playerInventory.numberOfKeys--;
                    Open();
                }
                // else{
                //     // dialogChange.dialog = "Uh Oh, you will need a key to unlock the door! It should be somewhere in other rooms. Happy searching!";
                // }
            }
        }   
    }

    public void Open(){
        Debug.Log("inside open");
        // set open to true
        open = true;
        // turn off door's box collider
        // physicsCollider.enabled = false; //might not need this
        door.SetActive(false);
        toRoom5.enabled = true; // this would be just enouugh

    }

    public void Close(){

    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = false;
        }
    }
}