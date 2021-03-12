using UnityEngine;

public class CutSceneController : MonoBehaviour
{
    [Header("Cutscene Over params")]
    [SerializeField] private GameObject thisObject;
    [SerializeField] private GameEvent cutsceneStartEvent;
    [SerializeField] private GameEvent cutsceneOverEvent;

    public void ActivateObject() {
        thisObject.SetActive(true);
    }

    public void DeactivateObject() {
        thisObject.SetActive(false);
    }
}

