using UnityEngine;

public class LockedDoor : InteractableObject {
    [Header("Player Inventory")]
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private InventoryItem keyItem;

    [Header("Dialog params")]
    [SerializeField] private bool dialogActive = false;
    [SerializeField] private StringValue doorLockedString;
    [SerializeField] private StringValue dialogText;
    [SerializeField] private GameEvent doorLockedEvent;

    [Header("Locker Door params")]
    [SerializeField] private bool doorOpen;
    [SerializeField] private BoolValue isOpen;
    [SerializeField] private Collider2D doorCollider;

    [Header("Unlock Door params")]
    [SerializeField] private UnlockDoor unlockDoorSprite;

    // [SerializeField] private SpriteRenderer doorSprite;

    // Start is called before the first frame update
    void Start() {
        // make the initial check to see if the door is open
        doorOpen = isOpen.value;
    }

    // Update is called once per frame
    void Update() {
        // check if the dorr isnt already open, and player within interaction zone
        if (!doorOpen && playerInRange) {
            if (Input.GetButtonDown("Interact")) {
                // check if the key item is in the player inventory
                if (PlayerHasKeyitem()) {
                    // use the item
                    playerInventory.UseItem(keyItem);
                    // open door
                    OpenDoor();
                } else {
                    dialogActive = !dialogActive;

                    dialogText.value = doorLockedString.value;

                    doorLockedEvent.Raise();
                }
            }
        }
    }

    bool PlayerHasKeyitem() {
        return playerInventory.canUnlock(keyItem);
    }

    void OpenDoor() {
        doorOpen = true;

        // send game event alert 
        gameEventClue.Raise();

        // USE CHANGESPRITE HERE OR CHANGE ANIMATION
        // doorSprite.enabled = false;

        unlockDoorSprite.DisplaySprite();
        doorCollider.enabled = false;
    }

    void CloseDoor() {
        doorOpen = false;

        // USE CHANGESPRITE HERE OR CHANGE ANIMATION
        // doorSprite.enabled = true;

        unlockDoorSprite.ChangeObjectSprite();
        doorCollider.enabled = true;
    }

    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            playerInRange = true;

            // only alert the listener if the door is closed (ready to interact)
            if (!doorOpen) {
                gameEventClue.Raise();
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            playerInRange = false;

            if (dialogActive) {
                // toggle
                dialogActive = !dialogActive;

                // send the game event alert
                doorLockedEvent.Raise();
            }

            // only alert the listener if the door is closed (ready to interact)
            if (!doorOpen) {
                gameEventClue.Raise();
            }
        }
    }
}
