//chris campbell - march 26, 2021
//this script manages the UI for the items equipped

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemsManager : MonoBehaviour
{
    public EquipmentManager equipmentManager;

    public Button headSlot;
    public Button chestSlot;
    public Button legsSlot;
    public Button weaponSlot;
    public Button shieldSlot;
    public Button feetSlot;

    // Update is called once per frame
    void Update()
    {
        //updates UI for current equipment for the head slot
        if (equipmentManager.currentEquipment[0] != null){
            headSlot.image.sprite = equipmentManager.currentEquipment[0].itemImage;
            headSlot.gameObject.SetActive(true);
        }
        else{
            headSlot.gameObject.SetActive(false);
        }
        //updates UI for current equipment for the chest slot
        if (equipmentManager.currentEquipment[1] != null){
            chestSlot.image.sprite = equipmentManager.currentEquipment[1].itemImage;
            chestSlot.gameObject.SetActive(true);
        }
        else{
            chestSlot.gameObject.SetActive(false);
        }
        //updates UI for current equipment for the legs slot
        if (equipmentManager.currentEquipment[2] != null){
            legsSlot.image.sprite = equipmentManager.currentEquipment[2].itemImage;
            legsSlot.gameObject.SetActive(true);
        }
        else{
            legsSlot.gameObject.SetActive(false);
        }
        //updates UI for current equipment for the weapon slot
        if (equipmentManager.currentEquipment[3] != null){
            weaponSlot.image.sprite = equipmentManager.currentEquipment[3].itemImage;
            weaponSlot.gameObject.SetActive(true);
        }
        else{
            weaponSlot.gameObject.SetActive(false);
        }
        //updates UI for current equipment for the shield slot
        if (equipmentManager.currentEquipment[4] != null){
            shieldSlot.image.sprite = equipmentManager.currentEquipment[4].itemImage;
            shieldSlot.gameObject.SetActive(true);
        }
        else{
            shieldSlot.gameObject.SetActive(false);
        }
        //updates UI for current equipment for the feet slot
        if (equipmentManager.currentEquipment[5] != null){
            feetSlot.image.sprite = equipmentManager.currentEquipment[5].itemImage;
            feetSlot.gameObject.SetActive(true);
        }
        else{
            feetSlot.gameObject.SetActive(false);
        }
    }
}
