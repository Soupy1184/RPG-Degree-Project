/*
    written by: Afieqha Mieza
    date: March 2021
    function: script to collect key
*/




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Powerup
{
    public Inventory playerInventory;
    // Start is called before the first frame update
    void Start()
    {
        // raise signal upon call
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // when collide with player
        if(other.CompareTag("Player") && !other.isTrigger){
            // increment of num of keys in inventory
            playerInventory.numberOfKeys += 1;

            // raise signal
            powerupSignal.Raise();

            // set object to inactive
            this.gameObject.SetActive(false);
        }
        
    }
}