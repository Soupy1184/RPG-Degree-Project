//needs to be deleted

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthSystem: MonoBehaviour {
    public int health;
    public int healthMax;

    public void SetHealth(int healthMax){
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth(){
        return health;
    }

    public void Damage (int damageAmount){
        health -= damageAmount;
        if (health < 0) {
            health = 0;
            gameObject.SetActive(false);
        }
    }

    public void Heal(int healAmount){
        health += healAmount;
        if (health > healthMax){
            health = healthMax;
        }
    }
}