using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Shop Inventory/Items")]
public class ShopKeeperItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool usable;
    public bool unique;
    
    public UnityEvent thisEvent;

    //in progress - uses the item
    public void Buy(){
        thisEvent.Invoke();
        Debug.Log(itemName + " has been bought.");
    }

    public void DecreaseAmount(int amount){
        numberHeld -= amount;
        if(numberHeld<0){
            numberHeld = 0;
        }
    }
}

