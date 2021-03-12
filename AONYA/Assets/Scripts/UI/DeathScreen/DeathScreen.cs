using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour {
    [Header("General")]
    [SerializeField] private string currScene;
    [SerializeField] private StringValue mainMenuString;
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private MenuButtonController menuButtonController;
    [SerializeField] private DeathScreenManager deathScreenManager;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private Animator transition;
    [SerializeField] private AnimatorFunctions animatorFunctions;
    [SerializeField] private int thisIndex;

    public static bool pressedRetry = false;

    void Start() {
        currScene = SceneManager.GetActiveScene().name;
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
                    pressedRetry = !pressedRetry;
                    
                    StartCoroutine(RetryCoroutine());
                } else if (thisIndex == 2) {
                    StartCoroutine(QuitToMenuCoroutiine());
                }
            } else if (animator.GetBool("pressed")) {
                animator.SetBool("pressed", false);
                menuButtonController.index = 0;
                animatorFunctions.disableOnce = true;
            }

        } else {
            animator.SetBool("selected", false);
        }
    }

    IEnumerator RetryCoroutine() {
        transition.SetTrigger("start");
        sceneLoader.LoadScene(currScene);

        yield return new WaitForSecondsRealtime(transition.speed);

        deathScreenManager.ChangeGiveUpValue();
    }


    IEnumerator QuitToMenuCoroutiine() {
        transition.SetTrigger("start");
        sceneLoader.LoadScene(mainMenuString.value);

        yield return new WaitForSecondsRealtime(transition.speed);

        deathScreenManager.ChangeGiveUpValue();
    }
}
