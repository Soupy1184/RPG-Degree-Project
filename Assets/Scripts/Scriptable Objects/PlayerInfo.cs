using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfo : ScriptableObject
{
    public float maxHealth;
    public float currentHealth;
    public string currentScene;
    public Vector2 currentPosition;
}
