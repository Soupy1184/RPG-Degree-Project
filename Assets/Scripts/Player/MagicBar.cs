//chris campbell - march 2021
//essentially duplicate from HealthBar.cs script
//resource: https://www.youtube.com/watch?v=BLfNP4Sc_iA

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public void SetMaxMana(float mana){
        slider.maxValue = mana;
    }
    
    public void SetMana(float mana){
        slider.value = mana;
    }
    
}
