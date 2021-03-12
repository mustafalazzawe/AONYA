using System;
using UnityEngine;

// this health class an be used for any gameobject in our world
public class Health : MonoBehaviour {
    [Header("Health params")]
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currHealth;
    [SerializeField] protected Collider2D hurtboxCollider;

    [SerializeField] protected GameObject deathEffect;

    // set health for gameobject
    public void SetHealth(int amountHealth) {
        currHealth = amountHealth;
    }

    // damage the object
    // virtual allows a subclass to override it 
    public virtual void Damage(int damageAmount) {
        currHealth -= damageAmount;

        // make sure the health doesnt decrease past 0
        if (currHealth <= 0) {
            currHealth = 0;
        }
    }

    // heal the game object
    protected void Heal(int healAmount) {
        currHealth += healAmount;
    }

    // heal the gameobject to its full health
    protected void FullHeal() {
        currHealth = maxHealth;
    }

    // kill the gameobject
    protected void Kill() {
        currHealth = 0;
    }

    protected virtual void Die() {
        // create the death effect 
        Instantiate(deathEffect, transform.position, transform.rotation);

        // set the enemy game object to false
        this.transform.parent.gameObject.SetActive(false);
    }
}
