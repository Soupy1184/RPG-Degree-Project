using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public Inventory playerInventory;
    public bool coinCounted;
     private GameObject coinSound;
    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
          if (GameObject.Find("coinCollect") != null) {
               coinSound = GameObject.Find("coinCollect");
          }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //add a coin, send the signal to the inventory, destroy the object
    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player") && !coinCounted){
               if (GameObject.Find("coinCollect") != null) {
                    coinSound.GetComponent<AudioSource>().Play();
               }
               playerInventory.coins += 1;
            powerupSignal.Raise();
            // Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            Debug.Log(other + "coin");
            coinCounted = true;
        }
        
    }
}
