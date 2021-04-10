//chris campbell - february 2021
//resource: https://www.youtube.com/watch?v=W10HidomWqs&list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu&index=56
//this script manages the player magic, currently not implemented in game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaReaction : MonoBehaviour
{
    public FloatValue playerMagic;
    public SignalSender magicSignal;
    
    public void Use(int amountToIncrease){
        playerMagic.RuntimeValue += amountToIncrease;
        magicSignal.Raise();
    }
}
