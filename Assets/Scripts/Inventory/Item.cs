//chris campbell - march 2021
//resource: https://www.youtube.com/watch?v=rtvuptLsEoY&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=76
//this script allows you to create new item scriptable objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool usable;
    public bool isEquipment;
    public bool unique;
    public bool isKey;
    public UnityEvent thisEvent;

    //add functionality in unity to customize the usage of item
    public virtual void Use(){
        thisEvent.Invoke();
    }

    //decrement the number player has in inventory
    public void DecreaseAmount(int amount){
        numberHeld -= amount;
        if(numberHeld < 0){
            numberHeld = 0;
        }
    }
}
