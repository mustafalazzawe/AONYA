using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour {
    [Header("Item params")]
    [SerializeField] private InventoryItem thisItem;
    [SerializeField] private InventoryController thisController;

    [Header("Inventory Slot params")]
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Image itemImage;

    [Header("Inventory Slot UI")]
    [SerializeField] private Transform slotScale;

    void Start() {
        ResizeSlot();
    }

    public void ResizeSlot() {
        // keep the scale consistent, dont scale depending on resolution
        Vector3 newScale;

        newScale = new Vector3(1.0f, 1.0f, 1.0f);

        slotScale.localScale = newScale;
    }

    // set up the inventory slot with a number held under the slot
    public void SetupWithNumberHeld(InventoryItem newItem, InventoryController newController) {
        thisItem = newItem;
        thisController = newController;

        if (thisItem) {
            itemImage.sprite = thisItem.itemImage;
            itemText.text = "" + thisItem.numberHeld;
        }
    }

    // set up the inventory slot with the items name under the slot
    public void SetupWithName(InventoryItem newItem, InventoryController newController) {
        thisItem = newItem;
        thisController = newController;

        if (thisItem) {
            itemImage.sprite = thisItem.itemImage;
            itemText.text = thisItem.itemName;
        }
    }

    // the slot act as buttons, once clicked update the description view
    public void ClickedOn() {
        if (thisItem) {
            thisController.SetupDescriptionAndBtn(thisItem);
        }
    }
}
