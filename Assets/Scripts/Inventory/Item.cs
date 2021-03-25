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
    public bool unique;
    public bool isKey;
    public UnityEvent thisEvent;

    public void Use(){
        thisEvent.Invoke();
    }

    public void DecreaseAmount(int amount){
        numberHeld -= amount;
        if(numberHeld < 0){
            numberHeld = 0;
        }
    }
}
