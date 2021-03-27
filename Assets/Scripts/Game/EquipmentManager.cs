using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class EquipmentManager : ScriptableObject, ISerializationCallbackReceiver
{
    public Equipment[] currentEquipment; 

    public Inventory equipmentInventory;

    public Equipment[] store;
    

    public void OnAfterDeserialize(){
        store = currentEquipment;
    }

    public void OnBeforeSerialize(){
        if(store != null){
            currentEquipment = store;
        }
        else{
            int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
            currentEquipment = new Equipment[numSlots];
        }
    }

    public void Equip (Equipment newItem){
        int slotIndex = (int) newItem.equipSlot;
        equipmentInventory.currentItem = newItem;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null){
            oldItem = currentEquipment[slotIndex];
            Debug.Log(oldItem);
            equipmentInventory.AddItem(oldItem);
        }

        currentEquipment[slotIndex] = newItem;
        equipmentInventory.items.Remove(equipmentInventory.currentItem);
    }
}
