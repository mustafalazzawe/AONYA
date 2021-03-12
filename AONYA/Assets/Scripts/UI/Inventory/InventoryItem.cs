using UnityEngine;
using UnityEngine.Events;

// this allows an asset to be serialized, meaning it can be saved to a file
[System.Serializable]
[CreateAssetMenu(fileName="New Inventory Item", menuName="Inventory/Inventory Item")]
public class InventoryItem : ScriptableObject {
    [Header("Item params")]
    public string itemName;
    public string itemType;
    public string itemDescription;
    public int numberHeld;
    public Sprite itemImage;
    [Space]
    public bool isEquipped;
    
    [Header("Item type")]
    public bool isKey;
    public bool isWeapon;
    public bool isDefault;
    public bool isEquipable;
    public bool isUsable;

    [Header("Item events")]
    [SerializeField] private UnityEvent thisEvent;

    // invokes an event whenever a button is pressed
    public void Pressed() {
        thisEvent.Invoke();
    }

    // if the item is used decrease its amount
    public void DecreaseAmount(int amountToDec) {
        numberHeld -= amountToDec;
        if(numberHeld < 0) {
            numberHeld = 0;
        }
    }
}
