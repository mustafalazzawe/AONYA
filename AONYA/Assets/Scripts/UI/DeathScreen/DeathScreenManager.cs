using UnityEngine;

public class DeathScreenManager : MonoBehaviour {
    [Header("Pause Manager params")]
    [SerializeField] private GameObject deathScreenUI;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private FloatValue gameSpeed;
    [SerializeField] private GameEvent playerDead;

    public static bool deathScreenActive = false;

    public void ChangeGiveUpValue() {
        deathScreenActive = !deathScreenActive;
        // toggle player movement/game speed
        PlayerMovement.freezeMovement = !PlayerMovement.freezeMovement;

        if (PlayerMovement.freezeMovement) {
            // set the pause menu object to active, showing the menu 
            deathScreenUI.SetActive(true);
            playerHUD.SetActive(false);
            gameSpeed.value = 0f;

            // set the time scale to 0, pausing all functions that are frame rate dependent
            Time.timeScale = gameSpeed.value;
        } else {
            // set the pause menu object to active, showing the menu 
            deathScreenUI.SetActive(false);
            playerHUD.SetActive(true);
            gameSpeed.value = 1f;

            // set the time scale to 1, resuming all functions that are frame rate dependent
            Time.timeScale = gameSpeed.value;
        }
    }
}
