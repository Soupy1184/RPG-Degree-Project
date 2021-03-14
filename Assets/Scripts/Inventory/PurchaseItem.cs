using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItem : MonoBehaviour
{
    public Inventory playerInventory;

    public void Buy(Item item){
        playerInventory.AddItem(item);
    }
}
