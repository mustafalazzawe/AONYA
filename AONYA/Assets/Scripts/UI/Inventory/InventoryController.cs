using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour {
    [Header("Inventory params")]
    public Inventory inventory;
    public InventoryItem currItem;

    [Header("Inventory scroll view")]
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryScroll;
    [SerializeField] private GameObject emptyInventory;

    [Header("Inventory description view")]
    [SerializeField] private GameObject equipBtn;
    [SerializeField] private GameObject unequipBtn;
    [SerializeField] private GameObject useBtn;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [Header("Inventory equipped panel")]
    [SerializeField] private GameObject blankEquippedSlot;
    [SerializeField] private GameObject weaponSlot;

    // set the items attributes, to update the description panel
    public void SetTextAndButton(string name, string description, bool btnActive, bool statsActive) {
        itemNameText.text = name;
        descriptionText.text = description;
        equipBtn.SetActive(btnActive);
        unequipBtn.SetActive(btnActive);
        useBtn.SetActive(btnActive);
    }

    // make the inventory slots in the scroll view
    void MakeInventorySlots() {
        // if an inventory exits
        if (inventory) {
            // for each item of the players inventory
            for (int i = 0; i < inventory.playerInventory.Count; i++) {
                if (inventory.playerInventory[i].numberHeld > 0) {
                    // create a slot, and set its parent to the scrollview
                    GameObject tempItem = Instantiate(blankInventorySlot, inventoryScroll.transform.position, Quaternion.identity);
                    tempItem.transform.SetParent(inventoryScroll.transform);

                    InventorySlot newSlot = tempItem.GetComponent<InventorySlot>();
                    if (newSlot) {
                        newSlot.SetupWithNumberHeld(inventory.playerInventory[i], this);
                    }
                }
            }
        }
    }

    // make the weapon slot in the equipement view
    void MakeWeaponSlot() {
        // if an inventory exits
        if (inventory) {
            if (inventory.equippedWeapon) {
                // set the slot to is full
                inventory.weaponSlotFull = true;

                GameObject tempItem = Instantiate(blankEquippedSlot, weaponSlot.transform.position, Quaternion.identity);
                tempItem.transform.SetParent(weaponSlot.transform);

                InventorySlot newSlot = tempItem.GetComponent<InventorySlot>();
                if (newSlot) {
                    newSlot.SetupWithName(inventory.equippedWeapon, this);
                }
            }
        }
    }

    // set the inventorys params once the inventory is toggled
    void OnEnable() {
        ClearInventorySlots();
        if (inventory.playerInventory.Count == 0) {
            emptyInventory.SetActive(true);
        } else {
            emptyInventory.SetActive(false);
        }

        MakeInventorySlots();
        MakeWeaponSlot();

        SetTextAndButton("", "", false, false);
    }

    // set up the description panel
    public void SetupDescriptionAndBtn(InventoryItem thisItem) {
        currItem = thisItem;
        itemNameText.text = thisItem.itemName;
        descriptionText.text = thisItem.itemDescription;

        if (!thisItem.isEquipped && !thisItem.isKey) {
            equipBtn.SetActive(true);
        } else {
            equipBtn.SetActive(false);
        }
        unequipBtn.SetActive(thisItem.isEquipped);
        useBtn.SetActive(thisItem.isUsable && !thisItem.isKey);
    }

    // a function to clear all the slots, acts as refresh
    void ClearInventorySlots() {
        for (int i = 0; i < inventoryScroll.transform.childCount; i++) {
            Destroy(inventoryScroll.transform.GetChild(i).gameObject);
        }
    }

    // a function to clear the weapon slot in the equipment panel
    public void ClearWeaponSlot() {
        inventory.weaponSlotFull = false;

        Destroy(weaponSlot.transform.GetChild(0).gameObject);
    }

    public void UseBtnClicked() {
        if (currItem) {
            currItem.Pressed();

            // clear all inventory slots
            ClearInventorySlots();

            // refill slots with new numbers 
            MakeInventorySlots();
            if (currItem.numberHeld == 0) {
                inventory.RemoveItem(currItem);

                SetTextAndButton("", "", false, false);
            }
        }
    }

    public void EquipBtnClicked() {
        if (currItem && currItem.isEquipped != true) {
            currItem.Pressed();

            if (inventory.weaponSlotFull) {
                inventory.equippedWeapon.isEquipped = false;
                inventory.equippedWeapon = null;

                ClearWeaponSlot();
            }

            if (currItem.isWeapon) {
                inventory.equippedWeapon = currItem;

                currItem.isEquipped = true;
                equipBtn.SetActive(false);
                unequipBtn.SetActive(true);

                MakeWeaponSlot();
            }

            // clear all inventory slots
            ClearInventorySlots();

            // refill slots with new numbers 
            MakeInventorySlots();

            SetTextAndButton("", "", false, false);
        }
    }

    public void UnequipBtnClicked() {
        if (currItem && currItem.isEquipped != false) {
            currItem.Pressed();

            currItem.isEquipped = false;
            equipBtn.SetActive(true);
            unequipBtn.SetActive(false);

            if (currItem.isWeapon) {
                inventory.equippedWeapon = null;

                ClearWeaponSlot();
            }

            // clear all inventory slots
            ClearInventorySlots();

            // refill slots with new numbers 
            MakeInventorySlots();
            SetTextAndButton("", "", false, false);
        }
    }
}
