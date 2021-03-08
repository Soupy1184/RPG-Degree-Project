using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int coins;
    public int maxMagic = 10;
    

    public void AddItem(Item item){
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
