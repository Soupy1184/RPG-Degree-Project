//chris campbell - april 2021
//this script allows you to create boolean valued scriptable objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class BooleanValue : ScriptableObject
{
    public bool initialValue;
    [HideInInspector]
    public bool RuntimeValue;

}