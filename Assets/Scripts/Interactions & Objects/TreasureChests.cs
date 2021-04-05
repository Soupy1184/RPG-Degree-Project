using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChests : MonoBehaviour
{
    [Header ("Treasure Chest")]
    // contents of the chest
    public Item contents;    
    // private Animator anim;
    public Inventory playerInventory;
    public GameObject chestOpened;
    // the chest closed sprite
    public GameObject chestClosed;

    [Header ("Connection Components")]
    // signal to send when chest is opened
    public SignalSender raiseItem;
    public SignalSender withdrawItem;
    
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
        // if F key is pressed and player is in range
        if(Input.GetKeyDown(KeyCode.F) && playerInRange){
            // if the chest is not yet open
            if(!isOpen){
                // open the chest
                OpenChest();
            }else{
                // tell progran that chest is already opened
                OpenedChest();
            }
        }
        
        // if(!playerInRange && isOpen){
        //     withdrawItem.Raise();
        //     // OpenedChest();
        // }
    }

    // open the chest
    public void OpenChest(){
        // add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // raise signal
        raiseItem.Raise();
        // set chest to open
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
        playerInventory.currentItem = null;
        // raise the signal to the player to stop animating
        withdrawItem.Raise();
    } 

    private void OnTriggerEnter2D(Collider2D other){
        // if chest collider collides with player collider, 
        // set playerInRange to true
        if(other.CompareTag("Player") && !other.isTrigger && !isOpen){
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = false;
        }
    }


}
