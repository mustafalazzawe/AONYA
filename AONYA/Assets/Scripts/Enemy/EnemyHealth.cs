using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health {
    // override damage function
    public override void Damage(int damageAmount) {
        base.Damage(damageAmount);

        // check if the enemys health is 0, die
        if (currHealth <= 0) {
            Die();
        }
    }

    protected override void Die() {
        base.Die();
    }
}
