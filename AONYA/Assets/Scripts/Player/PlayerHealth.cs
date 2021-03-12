using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerHealth : Health {
    [Header("Player")]
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Light2D playerPointLight;
    [SerializeField] private StateMachine playerState;

    [Header("Player Health params")]
    [SerializeField] private FloatValue maxPlayerHealth;
    [SerializeField] private FloatValue currPlayerHealth;
    [Space]
    [SerializeField] private DamageFlash flash;
    [Space]
    [SerializeField] private float deathDelay;
    [SerializeField] private GameEvent updateHearts;
    [SerializeField] private GameEvent playerDead;

    public static bool isPlayerDead;

    // Start is called before the first frame update
    void Start() {
        isPlayerDead = false;
        // set the initial health
        SetHealth((int)maxPlayerHealth.value);
        currPlayerHealth.value = maxPlayerHealth.value;

        hurtboxCollider.enabled = true;

        // invoke the event to update the heart HUD
        updateHearts.Raise();
    }

    // override the damage function in Health
    public override void Damage(int damageAmount) {
        base.Damage(damageAmount);

        // decrement damage from the current health
        currPlayerHealth.value -= damageAmount;

        // update the hearts 
        updateHearts.Raise();

        if (currHealth > 0) {
            if (flash) {
                flash.Flash();
            }
        } else if (currHealth == 0) {
            StartCoroutine(PLayerDeathCoroutine());

            playerState.ChangeState(GeneralState.dead);

            hurtboxCollider.enabled = false;
        }
    }

    protected override void Die() {
        // create the death effect 
        Instantiate(deathEffect, transform.position, transform.rotation);

        isPlayerDead = true;

        // set the enemy game object to false, we do this instead of setting 
        // the whole player inactive do to the camera constraints 
        playerSprite.enabled = false;
        playerPointLight.color = Color.red;
    }

    IEnumerator PLayerDeathCoroutine() {
        Die();

        yield return new WaitForSecondsRealtime(deathDelay);

        playerDead.Raise();
    }
}
