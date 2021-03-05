using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot{
    public Powerup thisLoot;

    //chance to spawn loot
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    //array of loot objects
    // public Loot[] loots;
}
