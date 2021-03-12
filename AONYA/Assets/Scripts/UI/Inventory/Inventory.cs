using System.Collections.Generic;
using UnityEngine;

// this allows an asset to be serialized, meaning it can be saved to a file
[System.Serializable]
[CreateAssetMenu(menuName = "Inventory/Player Inventory", fileName = "New PlayerInventory")]
public class Inventory : ScriptableObject {
    [Header("Inventory params")]
    [Space]
    // [SerializeField] private InventoryController inventoryController;
    [Header("Equipped Items")]
    public bool weaponSlotFull;
    public InventoryItem equippedWeapon;
    [Header("Item List")]
    public List<InventoryItem> playerInventory = new List<InventoryItem>();

    // public void ResetInventory() {
    //     for(int i = 0; i <= playerInventory.Count; --i) {
    //         if (!item.isDefault) {
    //             if(item.isEquipped) {
    //                 item.isEquipped = false;
    //                 equippedWeapon = null;

    //                 weaponSlotFull = false;
    //             }

    //             item.numberHeld = 0;

    //             RemoveItem(item);
    //         } 
    //     }
    // }

    public void AddItem(InventoryItem thisItem) {
        // check if the item is already in the inventory (can potentially handle unique items later), 
        // add item, else increase the number held
        if (!playerInventory.Contains(thisItem)) {
            playerInventory.Add(thisItem);

            thisItem.numberHeld = 1;
        } else {
            thisItem.numberHeld++;
        }
    }

    public void RemoveItem(InventoryItem thisItem) {
        // check if the item is in the inventory, removes 
        if (playerInventory.Contains(thisItem)) {
            playerInventory.Remove(thisItem);
        }
    }

    public void UseItem(InventoryItem thisItem) {
        // similar to remove but for usable items
        if (playerInventory.Contains(thisItem)) {
            if (thisItem.numberHeld > 0 && thisItem.isUsable) {
                thisItem.numberHeld--;

                if (thisItem.numberHeld <= 0) {
                    RemoveItem(thisItem);
                }
            }
        }
    }

    // checks if the list contains the items, returns true or false depending on the result
    public bool IsItemInInventory(InventoryItem thisItem) {
        return playerInventory.Contains(thisItem);
    }

    // check if the item is usable
    public bool canUseItem(InventoryItem thisItem) {
        return thisItem.numberHeld > 0 && thisItem.isUsable;
    }

    // check if the item is a key 
    public bool canUnlock(InventoryItem thisItem) {
        return thisItem.numberHeld > 0 && thisItem.isUsable && thisItem.isKey;
    }
}
