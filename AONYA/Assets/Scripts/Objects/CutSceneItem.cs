using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneItem : MonoBehaviour {
    [Header("Cut Scene Item params")]
    [SerializeField] private StringValue playerTag;
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameEvent cutsceneItem;

    public void PickUpItem(InventoryItem thisItem) {
        inventory.AddItem(thisItem);

        if (inventory.weaponSlotFull) {
            inventory.equippedWeapon.isEquipped = false;
            inventory.equippedWeapon = null;
        }

        inventory.equippedWeapon = thisItem;
        thisItem.isEquipped = true;

        inventory.weaponSlotFull = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            cutsceneItem.Raise();
        }
    }
}

