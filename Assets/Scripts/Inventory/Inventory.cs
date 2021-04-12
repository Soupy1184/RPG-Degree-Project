//chris campbell - march 2021
//resource: https://www.youtube.com/watch?v=rtvuptLsEoY&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=76
//this script allows you to create scriptable object inventories
//in game currently:
//- player inventory
//- shopkeeper inventory
//- equipment inventory

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
[System.Serializable]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;
    public int maxMagic = 10;

    //adds item to inventory
    public void AddItem(Item item){
        //check to see if it is a key
        if(item.isKey){
            numberOfKeys++;
        }
        else{
            //if item is not a key and item does not already exist in inventory
            if(!items.Contains(item)){
                items.Add(item);
            }
        }
    }
}
