using UnityEngine;
using UnityEngine.Audio;

public class MenuButton : MonoBehaviour {
    [Header("General")]
    [SerializeField] private int thisIndex;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private MenuButtonController menuButtonController;
    [SerializeField] private Animator animator;
    [SerializeField] private GameEvent gameStateEvent;

    // Update is called once per frame
    void Update() {
        // check the if the current index is this index 
        if (menuButtonController.index == thisIndex) {
            animator.SetBool("selected", true);

            // check if the enter btn is pressed 
            if (Input.GetAxis("Submit") == 1) {
                animator.SetBool("pressed", true);

                // check if enter is pressed on any of the indecies
                // index 1 - start, index 2 - load, index 3 - quit
                if (thisIndex == 1) {
                    sceneLoader.StartGame();

                    // gameStateEvent.Raise();
                    // Debug.Log("PRESSED: START GAME");
                } else if (thisIndex == 2) {
                    sceneLoader.LoadGame();
                    // Debug.Log("PRESSED: LOAD GAME");
                } else if (thisIndex == 3) {
                    sceneLoader.QuitGame();
                    Debug.Log("PRESSED: QUIT GAME");
                }

            } else if (animator.GetBool("pressed")) {
                animator.SetBool("pressed", false);
                menuButtonController.index = 0;
            }

        } else {
            animator.SetBool("selected", false);
        }
    }
}
