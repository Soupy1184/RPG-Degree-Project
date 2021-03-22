using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChests : MonoBehaviour
{
    [Header ("Treasure Chest")]
    // contents of the chest
    public Item contents;
    // the chest opened sprite
    public GameObject chestOpened;
    // the chest closed sprite
    public GameObject chestClosed;

    [Header ("Connection Components")]
    // signal to send when chest is opened
    public SignalSender raiseItem;
    // player inventory
    public Inventory playerInventory;
    
    [Header("Checking")]
    // checks if player is in range
    public bool playerInRange;
    // checks if chest is opened
    private bool isOpen; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if player is in range and key F is pressed
        if(Input.GetKeyDown(KeyCode.F) && playerInRange){
            // if is not yet open, open
            if(!isOpen){
                OpenChest();
            }else{
                OpenedChest();
            }
        }
    }

    // open the chest
    public void OpenChest(){
        // add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // raise signal
        // set chest to opened
        isOpen = true;

        //change the state of the chest
        chestClosed.SetActive(false);
        chestOpened.SetActive(true);

        // raise context clue
    }

    // is the chest already opened?
    public void OpenedChest(){
        // off dialogs
        // set contents to empty
        // playerInventory.currentItem = null;
    } 

    private void OnTriggerEnter2D(Collider2D other){
        // if chest collider collides with player collider, 
        // set playerInRange to true
        if(other.CompareTag("Player") && !other.isTrigger && !isOpen){
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        // if chest collider does not collides with player collider, 
        // set playerInRange to false
        if(other.CompareTag("Player") && !other.isTrigger && !isOpen){
            playerInRange = false;
        }
    }


}
