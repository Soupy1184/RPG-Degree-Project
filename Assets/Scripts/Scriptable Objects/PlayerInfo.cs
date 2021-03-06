using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    public int maxHealth;
    public int currentHealth;
    public string currentScene;
    public Vector2 currentPosition;
}
