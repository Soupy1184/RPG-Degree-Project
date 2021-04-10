//chris campbell - march 24, 2021
//manages the shopkeeper items, allows you to buy items from the shop

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Inventory/Items")]
public class ShopKeeperItem : Item
{
    public Inventory playerInventory;
    public int cost;

    public void Buy(int amount){
        if (numberHeld > 0){
            if(playerInventory.coins >= cost){
                numberHeld -= amount;
                if(numberHeld < 0){
                    numberHeld = 0;
                }
            }
            else {
                Debug.Log("Not Enough Coins!");
            }
        }
        else {
            Debug.Log("Shop does not have any " + itemName + " left.");
        }   
    }

    //need to be integrated into BUY function
    // public void AddItem(Item item){
    //     if (playerInventory.coins >= amount){
    //         if(playerInventory && item){
    //             if(playerInventory.items.Contains(item)){
    //                 item.numberHeld += 1;
    //                 playerInventory.coins -= amount;
    //                 coin.Raise();
    //             }
    //             else{
    //                 playerInventory.items.Add(item);
    //                 item.numberHeld += 1;
    //                 playerInventory.coins -= amount;
    //                 coin.Raise();
    //             }
    //         }
    //     }
    //     else {
    //         Debug.Log("Not enough coins!");
    //     }
        
    // }

}

