using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour {
    [Header("Event Listener params")]
    [SerializeField] public UnityEvent thisEvent;
    [SerializeField] public GameEvent thisGameEvent;

    public void OnEnable() {
        thisGameEvent.AddListener(this);
    }

    private void OnDisable() {
        thisGameEvent.RemoveListener(this);
    }

    // invokes the event
    public void Raise() {
        thisEvent.Invoke();
    }
}
