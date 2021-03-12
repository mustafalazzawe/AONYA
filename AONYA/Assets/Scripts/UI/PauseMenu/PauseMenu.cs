using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [Header("General")]
    [SerializeField] private int thisIndex;
    [SerializeField] private StringValue mainMenuString;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private MenuButtonController menuButtonController;
    [SerializeField] private PauseManager pauseManager;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private Animator transition;

    void Awake() {
        if (menuButtonController == null) {
            Debug.Log("MENU CONTROLLER NULL: " + this.name);
            menuButtonController = GetComponent<MenuButtonController>();
        }
    }

    // Update is called once per frame
    void Update() {
        // check the if the current index is this index 
        if (menuButtonController.index == thisIndex) {
            animator.SetBool("selected", true);

            // check if the enter btn is pressed 
            if (Input.GetAxisRaw("Submit") == 1) {
                animator.SetBool("pressed", true);

                // check if enter is pressed on any of the indeciesssa
                // index 1 - resume, index 2 - quit
                if (thisIndex == 1) {
                    pauseManager.ChangePauseValue();
                } else if (thisIndex == 2) {
                    StartCoroutine(SaveToMenuCoroutiine());
                } else if (thisIndex == 3) {
                    StartCoroutine(QuitToMenuCoroutiine());
                }
            } else if (animator.GetBool("pressed")) {
                animator.SetBool("pressed", false);
                menuButtonController.index = 0;
            }

        } else {
            animator.SetBool("selected", false);
        }
    }

    IEnumerator SaveToMenuCoroutiine() {
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
        // PlayerPrefs.SetFloat("PLayerX", playerPosition.value.x);
        // PlayerPrefs.SetFloat("PLayerY", playerPosition.value.y);

        transition.SetTrigger("start");
        sceneLoader.LoadScene(mainMenuString.value);

        yield return new WaitForSecondsRealtime(transition.speed);

        pauseManager.ChangePauseValue();
    }

    IEnumerator QuitToMenuCoroutiine() {
        transition.SetTrigger("start");
        sceneLoader.LoadScene(mainMenuString.value);

        yield return new WaitForSecondsRealtime(transition.speed);

        pauseManager.ChangePauseValue();
    }
}
