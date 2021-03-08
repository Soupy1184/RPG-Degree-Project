using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Item item;

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            AddItem();
            Destroy(this.gameObject);
        }
    }

    //picking up items in the world
    private void AddItem(){
        if(playerInventory && item){
            if(playerInventory.items.Contains(item)){
                item.numberHeld += 1;
            }
            else{
                playerInventory.items.Add(item);
                item.numberHeld += 1;
            }
        }
    }
}
