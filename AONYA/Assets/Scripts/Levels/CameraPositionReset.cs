using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionReset : MonoBehaviour
{
    [Header("Camera Position params")]
    [SerializeField] private VectorValue cameraPosition;

    // Start is called before the first frame update
    void Start() {
        // if a default player position exits, reset the players position
        if(cameraPosition) {
            transform.position = cameraPosition.value;
        }   
    }
}
