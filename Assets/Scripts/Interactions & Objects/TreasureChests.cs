/*
    written by: Afieqha Mieza
    date: March 2021
    function: script for treasure chest functionalities
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChests : MonoBehaviour
{
    [Header ("Treasure Chest")]
    public Item contents;    
    public GameObject chestOpened;
    public GameObject chestClosed;

    [Header ("Connection Components")]
    public SignalSender raiseItem;
    public SignalSender withdrawItem;
    public Inventory playerInventory;
    
    [Header("Checking")]
    public bool playerInRange;
    private bool isOpen; 

    // [Header("Sound Effects")]
    private SfxManager sfxMan;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // connecting to sound effect script
        sfxMan = FindObjectOfType<SfxManager>();
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
                sfxMan.openChest.Play();
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
        // add contents to inventory and current item
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // raise signal
        raiseItem.Raise();

        // set chest to open
        isOpen = true;

        //change the state of the chest sprite
        chestClosed.SetActive(false);
        chestOpened.SetActive(true);
    }

    // chest is opened
    public void OpenedChest(){
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
        // if chest collider is not colliding with player collider, 
        // set playerInRange to false
        if(other.CompareTag("Player") && !other.isTrigger){
            playerInRange = false;
        }
    }


}
