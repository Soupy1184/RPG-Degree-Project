//chris campbell - march 26, 2021

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedItemsManager : MonoBehaviour
{
    public EquipmentManager equipmentManager;
    Sprite head;
    Sprite chest;
    Sprite legs;
    Sprite weapon;
    Sprite shield;
    Sprite feet;

    [SerializeField] private Sprite headIcon;
    Sprite chestIcon;
    Sprite legsIcon;
    Sprite weaponIcon;
    Sprite shieldIcon;
    Sprite feetIcon;

    public Image headSlot;

    public Image chestSlot;
    public Image legsSlot;
    public Image weaponSlot;
    public Image shieldSlot;
    public Image feetSlot;
    // Start is called before the first frame update
    void Start()
    {
        if(head != null){
            head = equipmentManager.currentEquipment[0].itemImage;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //updates UI for current equipment for the head slot
        if (equipmentManager.currentEquipment[0] != null){
            headSlot.sprite = equipmentManager.currentEquipment[0].itemImage;
        }
        else {
            headSlot.sprite = headIcon;
        }
        
    }
}
