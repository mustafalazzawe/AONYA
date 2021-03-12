using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStrength : MonoBehaviour {
    public void AttackDamage(Health victimsHealth, int damageToGive) {
        // if the attacked gameobject has health
        if (victimsHealth) {
            victimsHealth.Damage(damageToGive);
        }
    }
}
