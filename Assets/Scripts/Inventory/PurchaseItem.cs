//chris campbell - march 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Inventory shopkeeperInventory;
    public SignalSender coin;
    private int amount = 15;

    //picking up items in the world
    public void AddItem(Item item){
        if (playerInventory.coins >= amount){
            if(playerInventory && item){
                if(playerInventory.items.Contains(item)){
                    item.numberHeld += 1;
                    playerInventory.coins -= amount;
                    coin.Raise();
                }
                else{
                    playerInventory.items.Add(item);
                    item.numberHeld += 1;
                    playerInventory.coins -= amount;
                    coin.Raise();
                }
            }
        }
        else {
            Debug.Log("Not enough coins!");
        }
        
    }
}

