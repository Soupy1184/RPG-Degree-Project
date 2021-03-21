//chris campbell - march 2021
//resource: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public FloatValue playerHealth;
    public SignalSender healthSignal;
    public PlayerInfo player;
    
    public void Use(int amountToIncrease){
        //chris campbell - updated march 19, 2021
        if (playerHealth.RuntimeValue >= player.maxHealth){
            playerHealth.RuntimeValue = player.maxHealth;
        }
        else{
            playerHealth.RuntimeValue += amountToIncrease;
            healthSignal.Raise();   
        }
    }
}