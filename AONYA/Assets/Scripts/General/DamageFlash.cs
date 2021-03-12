using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour {
    [Header("Flash params")]
    [SerializeField] private int numOfFlashes;
    [SerializeField] private float flashDelay;
    [SerializeField] private SpriteRenderer objSprite;
    [SerializeField] private Color flashColor;

    private bool isFlashing;

    public void Flash() {
        // check if the game object is already flashing
        if(!isFlashing) {
            StartCoroutine(FlashCoroutine());
        }
    }

    // create a delay between each flash (flash till completion before returning)
    public IEnumerator FlashCoroutine() {
        isFlashing = true;

        // flash per numOfFlashes
        for(int i = 0; i < numOfFlashes; i++) {
            // check if theres a sprite for the game object
            if(objSprite) {
                objSprite.color = flashColor;
                yield return new WaitForSeconds(flashDelay);

                objSprite.color = Color.white;
                yield return new WaitForSeconds(flashDelay);
            }
        }

        isFlashing = false;
    }
}
