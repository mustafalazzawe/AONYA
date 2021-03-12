using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BossHealth : Health {
    [Header("Demon")]
    [SerializeField] private SpriteRenderer demonSprite;
    [SerializeField] private Light2D demonPointLight;
    [SerializeField] private StateMachine demonState;

    [Header("Demon Health params")]
    [SerializeField] private FloatValue maxDemonHealth;
    [SerializeField] private FloatValue currDemonHealth;
    [Space]
    [SerializeField] private DamageFlash flash;
    [Space]
    [SerializeField] private float deathDelay;
    [SerializeField] private GameEvent updateHearts;
    [SerializeField] private GameEvent demonHit;
    [SerializeField] private GameEvent demonDead;

    public static bool isDemonDead;

    // Start is called before the first frame update
    void Start() {
        isDemonDead = false;
        // set the initial health
        SetHealth((int)maxDemonHealth.value);
        currDemonHealth.value = maxDemonHealth.value;

        hurtboxCollider.enabled = true;

        // invoke the event to update the heart HUD
        updateHearts.Raise();
    }

    // override the damage function in Health
    public override void Damage(int damageAmount) {
        base.Damage(damageAmount);

        // decrement damage from the current health
        currDemonHealth.value -= damageAmount;

        // demon hit
        demonHit.Raise();

        // update the hearts 
        updateHearts.Raise();

        if (currHealth > 0) {
            if (flash) {
                flash.Flash();
            }
        } else if (currHealth == 0) {
            isDemonDead = !isDemonDead;

            StartCoroutine(DemonDeathCoroutine());

            demonState.ChangeState(GeneralState.dead);

            hurtboxCollider.enabled = false;
        }
    }

    protected override void Die() {
        // create the death effect 
        Instantiate(deathEffect, transform.position, transform.rotation);

        // set the enemy game object to false, we do this instead of setting 
        // the whole demon inactive do to the camera constraints 
        demonSprite.enabled = false;
        demonPointLight.color = Color.cyan;
    }

    IEnumerator DemonDeathCoroutine() {
        Die();

        yield return new WaitForSecondsRealtime(deathDelay);

        demonDead.Raise();
    }
}