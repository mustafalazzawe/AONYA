using UnityEngine;

public class PlayerPositionReset : MonoBehaviour {
    [Header("Player Position params")]
    [SerializeField] private VectorValue playerPosition;

    // Start is called before the first frame update
    void Start() {
        // if a default player position exits, reset the players position
        if(playerPosition) {
            transform.position = playerPosition.value;
        }   
    }
}
