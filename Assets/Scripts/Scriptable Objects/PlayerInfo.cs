//chris campbell - march 2021
//this script manages the player information that needs to be stored

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlayerInfo : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public string currentScene;
    public Vector2 currentPosition;
}
