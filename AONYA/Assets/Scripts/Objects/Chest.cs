using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject {
    [Header("Chest params")]
    [SerializeField] private bool chestOpen;
    [SerializeField] private BoolValue isOpen;
    [SerializeField] private AnimatorController chestAnim;
    [SerializeField] private GameEvent chestOpenEvent;

    [Header("Item params")]
    [SerializeField] private StringValue dialogText;
    [SerializeField] private SpriteValue itemSprite;
    [SerializeField] private InventoryItem thisItem;
    [SerializeField] private Inventory playerInventory;

    // Start is called before the first frame update
    void Start() {
        // make the initial check to see if the chest is open
        chestOpen = isOpen.value;
        if (chestOpen) {
            chestAnim.SetAnimParameter("open", true);
        }
    }

    // Update is called once per frame
    void Update() {
        // check if the play is within the interaction zone
        if (playerInRange) {
            // check for interaction button "i"
            if (Input.GetButtonUp("Interact")) {
                // check if the chest is already open, do nothing, else get contents
                if (chestOpen) {
                    return;
                }
                GetChestItem();
            }
        }
    }

    void GetChestItem() {
        // toggle chestOpen
        chestOpen = !chestOpen;
        // play open animation
        chestAnim.SetAnimParameter("open", true);

        // reset the default value
        isOpen.value = chestOpen;

        // set the chest item sprite
        itemSprite.value = thisItem.itemImage;

        // set the chest item description
        dialogText.value = thisItem.itemDescription;

        // raise the chest open game event (received item)
        chestOpenEvent.Raise();

        // add the item to the players inventory
        playerInventory.AddItem(thisItem);

        // end the player event clue
        gameEventClue.Raise();
    }

    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            playerInRange = true;

            // only raise the game event clue if the chest is closed
            if (!chestOpen) {
                gameEventClue.Raise();
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            playerInRange = false;

            // only raise the game event clue if the chest is closed
            if (!chestOpen) {
                gameEventClue.Raise();
            }
        }
    }
}
