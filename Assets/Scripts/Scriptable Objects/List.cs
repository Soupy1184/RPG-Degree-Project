//chris campbell - april 2021
//this script allows you to create a scriptable object to store a list

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class List : ScriptableObject
{
    public List<ScriptableObject> list = new List<ScriptableObject>();

    public List<ScriptableObject> RuntimeValue;

    
}
