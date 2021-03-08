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
    public Item thisItem;
    public InventoryManager thisManager;

    //setups the item to be placed in inventory
    public void Setup(Item newItem, InventoryManager newManager){
        //grabs scriptable object info and manager
        this.thisItem = newItem;
        this.thisManager = newManager;

        //sets scriptable object info
        if(thisItem){
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    //sets text and shows button in invetory manager when clicked
    public void OnClick(){
        if(thisItem){
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
        }
    }
}
