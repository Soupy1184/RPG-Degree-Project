using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    // public static EquipmentManager instance;

    // void Awake(){

    // }
    

    public Equipment[] currentEquipment;

    public Inventory equipmentInventory;

    void Start(){
        

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

       
    }

    public void Equip (Equipment newItem){
        int slotIndex = (int) newItem.equipSlot;
        equipmentInventory.currentItem = newItem;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null){
            oldItem = currentEquipment[slotIndex];
            equipmentInventory.AddItem(oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        equipmentInventory.items.Remove(equipmentInventory.currentItem);
    }
}
