using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    [Header("Pause Manager params")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private FloatValue gameSpeed;

    public static bool inventoryScreenActive = false;

    // Update is called once per frame
    void Update() {
        if (!DeathScreenManager.deathScreenActive) {
            if (!PauseManager.pauseMenuActive) {
                if (Input.GetButtonUp("Inventory")) {
                    ChangeInventoryValue();
                }
            }
        }
    }

    public void ChangeInventoryValue() {
        // toggle player movement/game speed
        PlayerMovement.freezeMovement = !PlayerMovement.freezeMovement;

        inventoryScreenActive = !inventoryScreenActive;

        if (PlayerMovement.freezeMovement) {
            // set the inventory object to active, showing the ui 
            inventoryUI.SetActive(true);
            playerHUD.SetActive(false);
            gameSpeed.value = 0f;

            // set the time scale to 0, pausing all functions that are frame rate dependent
            Time.timeScale = gameSpeed.value;
        } else {
            // set the inventory object to active, showing the ui 
            inventoryUI.SetActive(false);
            playerHUD.SetActive(true);
            gameSpeed.value = 1f;

            // set the time scale to 1, resuming all functions that are frame rate dependent
            Time.timeScale = gameSpeed.value;
        }
    }
}
