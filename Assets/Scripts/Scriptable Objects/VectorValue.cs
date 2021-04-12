//chris campbell - february 2021
//resource: https://www.youtube.com/watch?v=wNl--exin90&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=30
//this script allows you to create a scriptable object to store a vector 2 values
//example: player [x, y] coordinates

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class VectorValue : ScriptableObject {
    public Vector2 initialValue;
    public Vector2 defaultValue;

    
}
