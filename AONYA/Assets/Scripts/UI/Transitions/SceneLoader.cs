using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    [Header("General")]
    [SerializeField] private float transitionTime = 1.5f;
    [SerializeField] private Animator transition;

    public void StartGame() {
        StartCoroutine(LoadSceneCoroutine(1));
        Debug.Log("Starting...");
    }

    public void LoadGame() {
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
        Debug.Log("Loading...");
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("Quitting...");
    }

    public void LoadNextScene() {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex) {
            StartCoroutine(LoadSceneCoroutine(nextSceneIndex));
        }
        // load next scene in list

    }

    public void LoadScene(string sceneName) {
        // load named scene
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public void LoadScene(StringValue sceneName) {
        // load named scene
        StartCoroutine(LoadSceneCoroutine(sceneName.value));
    }

    // delay the loader
    IEnumerator LoadSceneCoroutine(int levelIndex) {
        // play animation
        transition.SetTrigger("start");

        if(transitionTime >= 1.5f) {
            transition.speed = 0.15f;
        }

        // wait 
        yield return new WaitForSeconds(transitionTime);

        // load
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadSceneCoroutine(string sceneName) {
        if(transitionTime >= 1.5f) {
            transition.speed = 0.15f;
        }

        // wait 
        yield return new WaitForSecondsRealtime(transitionTime);

        // load
        SceneManager.LoadScene(sceneName);
    }
}
