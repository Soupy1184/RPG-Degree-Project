//chris campbell - march 2021
//resource: 
//this script manages the player losing/gaining health

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
        //player should not be allowed to go over max health 
        if (playerHealth.RuntimeValue >= player.maxHealth){
            playerHealth.RuntimeValue = player.maxHealth;
        }
        else{
            playerHealth.RuntimeValue += amountToIncrease;
            healthSignal.Raise();   
        }
    }
}