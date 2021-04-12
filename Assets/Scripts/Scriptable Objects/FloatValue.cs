/*
    written by: Afieqha Mieza
    date: January 2021
    //this script allows you to create a scriptable object to store a float value
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class FloatValue : ScriptableObject
{
    public float initialValue;
    [HideInInspector]
    public float RuntimeValue;

  
}
