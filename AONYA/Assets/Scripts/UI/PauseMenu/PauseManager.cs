using System;
using UnityEngine;

public class PauseManager : MonoBehaviour {
    [Header("Pause Manager params")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private FloatValue gameSpeed;

    public static bool pauseMenuActive = false;

    // Update is called once per frame
    void Update() {
        if (!DeathScreenManager.deathScreenActive) {
            if (!InventoryManager.inventoryScreenActive) {
                if (Input.GetButtonUp("Pause")) {
                    ChangePauseValue();
                }
            }
        }
    }

    public void ChangePauseValue() {
        // toggle player movement/game speed
        PlayerMovement.freezeMovement = !PlayerMovement.freezeMovement;

        pauseMenuActive = !pauseMenuActive;

        if (PlayerMovement.freezeMovement) {
            // set the pause menu object to active, showing the menu 
            pauseMenu.SetActive(true);
            playerHUD.SetActive(false);
            gameSpeed.value = 0f;

            // set the time scale to 0, pausing all functions that are frame rate dependent
            Time.timeScale = gameSpeed.value;
        } else {
            // set the pause menu object to active, showing the menu 
            pauseMenu.SetActive(false);
            playerHUD.SetActive(true);
            gameSpeed.value = 1f;

            // set the time scale to 1, resuming all functions that are frame rate dependent
            Time.timeScale = gameSpeed.value;
        }
    }
}
