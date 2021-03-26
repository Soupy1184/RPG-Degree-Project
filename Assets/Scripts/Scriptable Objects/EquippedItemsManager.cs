using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class EquippedItemsManager : ScriptableObject
{
    EquipmentManager equipment;
    public Equipment head;

    void Update(){
        head = equipment.currentEquipment[0];
    }
   
}
