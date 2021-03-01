using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variable from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    //setups the item to be placed in inventory
    public void Setup(InventoryItem newItem, InventoryManager newManager){
        //grabs scriptable object info and manager
        this.thisItem = newItem;
        this.thisManager = newManager;

        //sets scriptable object info
        if(thisItem){
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void OnClick(){
        if(thisItem){
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
        }
    }
}
