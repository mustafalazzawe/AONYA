using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Game Event", fileName = "New Game Event")]
public class GameEvent : ScriptableObject {
    [Header("Game Event listeners")]
    public List<EventListener> eventListeners = new List<EventListener>();

    // lets the listener know to invoke the game event
    public void Raise() {
        // traverse the list of listers backwards for efficiency
        for (int i = eventListeners.Count - 1; i >= 0; i--) {
            eventListeners[i].Raise();
        }
    }

    public void AddListener(EventListener eventListener) {
        // check if the list already contains the listener, if not add to list
        if(!eventListeners.Contains(eventListener)) {
            eventListeners.Add(eventListener);
        }
    }

    public void RemoveListener(EventListener eventListener) {
        // check to see if it contains the listener, if yes remove
        if(eventListeners.Contains(eventListener)) {
            eventListeners.Remove(eventListener);
        }
    }
}
