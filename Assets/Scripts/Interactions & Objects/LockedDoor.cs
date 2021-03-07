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

public class LockedDoor : Dialog
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    // public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;

    private void Update (){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(playerInRange && thisDoorType == DoorType.key){
                //checks for key
                if(playerInventory.numberOfKeys > 0){
                    // remove a player key
                    playerInventory.numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open(){
        // set open to true
        open = true;
        // turn off door's box collider
        physicsCollider.enabled = false;
    }

    public void Close(){

    }
}