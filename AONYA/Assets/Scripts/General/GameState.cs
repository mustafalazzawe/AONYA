using UnityEngine;

// this class will handle any game state events (pause, quit, restart, etc)
public class GameState : MonoBehaviour {
    [Header("Game State params")]
    [SerializeField] private GameEvent gameStateEvent;
    [Header("Game elements")]
    [SerializeField] private Inventory inventory;

    public void NewGame() {
        // reset inventory 
        
    }
}