/*
    written by: Afieqha Mieza
    date: March 2021
    function: script to collect coins
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    [Header("Player Inventory")]
    public Inventory playerInventory;
    public bool coinCounted;
    

    // [Header("Sound Effects")]
    private GameObject coinSound;
    private SfxManager sfxMan;

    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
        if (GameObject.Find("coinCollect") != null) {
            coinSound = GameObject.Find("coinCollect");
        }

        // reference to sound effect script
        sfxMan = FindObjectOfType<SfxManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //add a coin, send the signal to the inventory, destroy the object
    void OnTriggerEnter2D(Collider2D other){
        // if collides with player and not yet counted
        if(other.CompareTag("Player") && !coinCounted){
            if (GameObject.Find("coinCollect") != null) {
                coinSound.GetComponent<AudioSource>().Play();
            }

            // update coin in inventory
            playerInventory.coins += 1;

            // play sound effect
            sfxMan.coins.Play();

            // raise signal
            powerupSignal.Raise();

            // set object to inactive once collected
            this.gameObject.SetActive(false);

            // refer to coin is already counted
            coinCounted = true;
        }
        
    }
}
