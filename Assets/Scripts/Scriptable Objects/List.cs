//chris campbell - april 2021
//this script allows you to create a scriptable object to store a list

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class List : ScriptableObject, ISerializationCallbackReceiver
{
    public List<ScriptableObject> list = new List<ScriptableObject>();

    public List<ScriptableObject> RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = list;
    }

    public void OnBeforeSerialize()
    {

    }
}
