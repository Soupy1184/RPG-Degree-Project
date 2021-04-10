//chris campbell - april 2021
//this script allows you to create boolean valued scriptable objects

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BooleanValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initialValue;
    [HideInInspector]
    public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}