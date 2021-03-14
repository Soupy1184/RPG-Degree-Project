using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerialiationCallbackReceiver
{
    public float initialValue;

    public void OnAfterDeserialize(){
        
    }
}
