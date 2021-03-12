using UnityEngine;

public class Sign : InteractableObject {
    [Header("Sign params")]
    [SerializeField] private bool dialogActive = false;
    [SerializeField] private string signText;
    [SerializeField] private GameEvent dialogGameEvent;
    [SerializeField] private StringValue dialogText;

    // Update is called once per frame
    void Update() {
        // checks if player is within interaction zone
        if (playerInRange) {
            if (Input.GetButtonDown("Interact")) {
                // toggle
                dialogActive = !dialogActive;

                // set the dialog text
                dialogText.value = signText;

                // send the game event alert
                dialogGameEvent.Raise();
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerExit2D(other);

        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            // checks if the dialog box is still active one you exit the interaction zone, close it
            if (dialogActive) {
                // toggle
                dialogActive = !dialogActive;

                // send the game event alert
                dialogGameEvent.Raise();
            }
        }
    }
}
