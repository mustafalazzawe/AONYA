using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour {
    [Header("Timeline Manager params")]
    [SerializeField] private GameEvent cutsceneStart;
    [SerializeField] private GameEvent cutsceneOver;
    [SerializeField] private PlayableDirector playableDirector;

    public static bool isCutscene;

    // Update is called once per frame
    void Update() {
        SceneObjects();
    }

    void SceneObjects() {
        if(!(playableDirector.state == PlayState.Playing)) {
            isCutscene = false;

            cutsceneOver.Raise();
        } else {
            isCutscene = true;

            cutsceneStart.Raise();
        }
    }
}
