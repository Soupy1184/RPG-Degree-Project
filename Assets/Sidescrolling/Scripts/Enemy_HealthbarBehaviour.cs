using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy_HealthbarBehaviour : MonoBehaviour
{

     public Slider slider;
     public Color low;
     public Color high;
     public Vector3 offset;
     public bool boss;
     public TextMeshProUGUI healthText;

     public void SetHealth(float health, float maxHealth) {
          slider.gameObject.SetActive(health < maxHealth && health > 0);
          slider.value = health;
          slider.maxValue = maxHealth;

          if (!boss) {
               slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
          }
          else {
               healthText.text = health + "/" + slider.maxValue;
          }
     }

    // Update is called once per frame
    void Update()
    {
          if (!boss) {
               slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
          }
    }
}
