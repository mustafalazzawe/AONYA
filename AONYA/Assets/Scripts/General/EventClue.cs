using UnityEngine;

public class EventClue : MonoBehaviour {
    [Header("Event Clue params")]
    [SerializeField] private bool clueActive = false;
    [SerializeField] private SpriteRenderer thisSprite;

    public void ChangeClue() {
        // toggle clue
        clueActive = !clueActive;

        // toggle the sprite renderer aswell
        thisSprite.enabled = clueActive;
    }
}
