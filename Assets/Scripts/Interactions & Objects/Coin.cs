using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public Inventory playerInventory;
    public bool coinCounted;
    // Start is called before the first frame update
    void Start()
    {
        powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("Player") && !coinCounted){
            playerInventory.coins += 1;
            powerupSignal.Raise();
            // Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            Debug.Log(other + "coin");
            coinCounted = true;
        }
        
    }
}
