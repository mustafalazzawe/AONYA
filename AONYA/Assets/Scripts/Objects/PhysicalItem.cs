using UnityEngine;

public class PhysicalItem : InteractableObject {
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;

    [Header("Item params")]
    [SerializeField] private GameObject itemObject;
    [SerializeField] private InventoryItem thisItem;

    // Start is called before the first frame update
    void Start() {
        itemObject.SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        // check if the player is in the interaction area
        if (playerInRange) {
            // if the "i" key is pressed 
            if (Input.GetButtonDown("Interact")) {
                inventory.AddItem(thisItem);
                itemObject.SetActive(false);
            }
        }
    }

    // item interaction zone - enter
    public override void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(playerTag.value)) {
            playerInRange = true;

            gameEventClue.Raise();
        }
    }

    // item interaction zone - exit
    public override void OnTriggerExit2D(Collider2D other) {
        base.OnTriggerEnter2D(other);

        if (other.CompareTag(playerTag.value)) {
            playerInRange = false;

            gameEventClue.Raise();
        }
    }
}
