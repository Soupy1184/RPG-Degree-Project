//chris campbell - march 24, 2021
//resource: https://www.youtube.com/watch?v=d9oLS5hy0zU&t=89s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipemnt", menuName = "Iventory/Equipment")]
public class Equipment : Item
{ 
    public EquipmentSlot equipSlot;

    public int armourModifier;
    public int damageModifier;
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }