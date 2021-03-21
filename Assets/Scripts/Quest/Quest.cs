//chris campbell - march 18, 2021
//resource: https://www.youtube.com/watch?v=e7VEe_qW4oE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public bool isActive;

    public string title;
    public string description;
    public int experienceReward;
    public int goldReward;
}
