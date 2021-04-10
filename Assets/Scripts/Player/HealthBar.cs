//chris campbell - march 2021
//resource: https://www.youtube.com/watch?v=BLfNP4Sc_iA
//this scripte manages the health bar UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI healthText;
    public void SetMaxHealth(float health){
        slider.maxValue = health;
    }
    
    public void SetHealth(float health){
        slider.value = health;
        healthText.text = health + "/" + slider.maxValue;
    }
}
