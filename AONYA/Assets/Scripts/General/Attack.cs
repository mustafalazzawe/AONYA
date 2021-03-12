using UnityEngine;

public class Attack : AttackStrength {
    [Header("Attack params")]
    [SerializeField] private StringValue victimString;
    [SerializeField] private int damageAmount;

    public void OnTriggerEnter2D(Collider2D other) {
        // if the collison happens with a gameobject with the victim tag
        if(other.gameObject.CompareTag(victimString.value)) {
            // get the victims current health
            Health victimCurrHealth = other.gameObject.GetComponent<Health>();

            // if the victim has a health, apply damage amount to it 
            if(victimCurrHealth) {
                AttackDamage(victimCurrHealth, damageAmount);
            }
        }
    }
}
