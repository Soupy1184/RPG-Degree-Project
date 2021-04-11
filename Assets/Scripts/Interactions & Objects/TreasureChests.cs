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

    public BooleanValue value;
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
        isOpen = value.initialValue;
        chestClosed.SetActive(!value.initialValue);
        chestOpened.SetActive(value.initialValue);
        // connecting to sound effect script
        sfxMan = FindObjectOfType<SfxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if F key is pressed and player is in range
        if(Input.GetKeyDown(KeyCode.F) && playerInRange){
            Debug.Log(isOpen);
            // if the chest is not yet open
            if(!isOpen){
                // open the chest
                OpenChest();
                sfxMan.openChest.Play();
                StartCoroutine(OpenedChest());
            }
        }
    }

    // open the chest
    public void OpenChest(){
        if(contents.isKey){
            playerInventory.numberOfKeys++;
        }else{
            //if item is not a key and item does not already exist in inventory
            if(playerInventory.items.Contains(contents)){
            contents.numberHeld += 1;
            }else{
                playerInventory.items.Add(contents);
                contents.numberHeld += 1;
            }
        }
        
        // add contents to inventory and current item
        // playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;

        // raise signal
        raiseItem.Raise();

        // set chest to open
        value.initialValue = true;

        //change the state of the chest sprite
        chestClosed.SetActive(!value.initialValue);
        chestOpened.SetActive(value.initialValue);
        isOpen = true;
    }

    // chest is opened
    IEnumerator OpenedChest(){
        yield return new WaitForSeconds(2f);
        // set contents to empty
        playerInventory.currentItem = null;

        // raise the signal to the player to stop animating
        withdrawItem.Raise();
    } 

    private void OnTriggerEnter2D(Collider2D other){
        // if chest collider collides with player collider, 
        // set playerInRange to true
        if(other.CompareTag("Player") && !other.isTrigger){
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
