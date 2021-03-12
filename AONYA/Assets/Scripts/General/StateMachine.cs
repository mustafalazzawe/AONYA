using UnityEngine;

// create a enum with the different possible states 
public enum GeneralState {
    idle,
    walk,
    attack,
    receiveItem,
    dead
}

public class StateMachine : MonoBehaviour {
    public GeneralState currState;

    // this function changes the current state of the gameobject
    public void ChangeState(GeneralState newState) {
        // check that the current state isnt the new state that its being changed to
        if (currState != newState) {
            currState = newState;
        }
    }
}
