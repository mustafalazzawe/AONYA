using UnityEngine;

public class CutSceneManager : MonoBehaviour {
    [Header("Cut Scene Management")]
    [SerializeField] private StringValue sceneName;
    [SerializeField] private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Update() {
        sceneLoader.LoadScene(sceneName.value);
    }
}
