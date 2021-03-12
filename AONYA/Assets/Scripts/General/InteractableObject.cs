using UnityEngine;

// every interactable object will have a trigger collider, acting as its interact range
[RequireComponent(typeof(BoxCollider2D))]
public class InteractableObject : MonoBehaviour {
    [Header("Interactable params")]
    [SerializeField] public bool playerInRange;
    [SerializeField] public StringValue playerTag;
    [SerializeField] public GameEvent gameEventClue;

    public virtual void OnTriggerEnter2D(Collider2D other) {
        //check if the player enters the interaction range
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            playerInRange = true;

            // once they are in range display some type of clue to let the player know
            // that you can interact with the item
            gameEventClue.Raise();
        }
    }

    // check if the player exits the interaction range
    public virtual void OnTriggerExit2D(Collider2D other) {
        //check if the player enters the interaction range
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            playerInRange = false;

            // update the event to end the clue
            gameEventClue.Raise();
        }
    }
}
