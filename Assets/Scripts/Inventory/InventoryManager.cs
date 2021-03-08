using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Iventory Information")]
    public Inventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public Item currentItem;

    // Called whenever the inventory is opened -> clear, create, set
    void OnEnable(){
        ClearInventorySlots(); 
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    //sets the given text and shows button if the item is usable
    public void SetTextAndButton(string description, bool buttonActive){
        descriptionText.text = description;
        if(buttonActive){
            useButton.SetActive(true);
        }
        else{
            useButton.SetActive(false);
        }
    }

    //overload for SetTextAndButton
    public void SetupDescriptionAndButton(string newDescriptionString, bool isButtonUsable, Item newItem){
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }

    //creates a new item in the inventory grid
    void MakeInventorySlots(){
        if(playerInventory){
            for(int i = 0; i < playerInventory.items.Count; i++){
                if (playerInventory.items[i].numberHeld > 0){
                //instantiate new item
                GameObject temp = Instantiate(blankInventorySlot, inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);
                InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                //add item in panel
                if(newSlot){
                    newSlot.Setup(playerInventory.items[i], this);
                }
                }
            }
        }
    }

    //clear inventory 
    private void ClearInventorySlots(){
        for(int i = 0; i < inventoryPanel.transform.childCount; i++){
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    //using an item
    public void UseButtonPressed(){
        if(currentItem){
            currentItem.Use();
            //clear all slot then refill
            ClearInventorySlots();
            MakeInventorySlots();
            if(currentItem.numberHeld == 0){
                SetTextAndButton("", false);
            }
        }
    }
}
