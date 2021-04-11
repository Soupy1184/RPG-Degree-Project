//chris campbell - march 27, 2021
//resource: https://www.youtube.com/watch?v=d9oLS5hy0zU&t=89s
//this script manages the equipment, equipping and unequipping

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

     public GameObject player;
    
    //when game closes down
    public void OnAfterDeserialize(){ 
        //stores the current equipment
        store = currentEquipment;
    }

    //when game starts up
    public void OnBeforeSerialize(){
        if(store != null){ //gets the equipment from storage array
            currentEquipment = store;
        }
        else{ //creates a new equipment if its the first time 
            //num slot is equal to the length of the enum 
            int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
            currentEquipment = new Equipment[numSlots];
        }
    }

    //Equips an item
    public void Equip (Equipment newItem){
        //gets the index of the enum 
        int slotIndex = (int) newItem.equipSlot;
        equipmentInventory.currentItem = newItem;

        Equipment oldItem = null;

        //swaps equipemtn if there is an equipment item equipped in the slot 
        //the equipment needs to go to
        if (currentEquipment[slotIndex] != null){
            oldItem = currentEquipment[slotIndex];
            equipmentInventory.AddItem(oldItem);
        }

        //equips equiment
        currentEquipment[slotIndex] = newItem;
        //removes item from equipment inventory
        equipmentInventory.items.Remove(equipmentInventory.currentItem);

          player = GameObject.Find("Player");

          if (player != null)
               player.GetComponent<Sidescrolling_PlayerController>().CalculateEquipment();
    }

    public void Unequip(int slotIndex){
        if (currentEquipment[slotIndex] != null){
            //find the equipment in the slot
            Equipment oldItem = currentEquipment[slotIndex];
            //add it back to you inventory
            equipmentInventory.AddItem(oldItem);
            
            currentEquipment[slotIndex] = null;
        }
          player = GameObject.Find("Player");

          if (player != null)
               player.GetComponent<Sidescrolling_PlayerController>().CalculateEquipment();
     }
}
