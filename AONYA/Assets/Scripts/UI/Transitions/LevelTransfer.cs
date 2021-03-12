using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransfer : MonoBehaviour {
    [Header("Level Transfer params")]
    [SerializeField] private Vector2 playerNewPosition;
    [SerializeField] private VectorValue playerLevelPosition;
    [SerializeField] private StringValue playerTag;
    [SerializeField] private StringValue levelString;
    [SerializeField] private SceneLoader sceneLoader;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            playerLevelPosition.value = playerNewPosition;

            sceneLoader.LoadScene(levelString.value);
        }
    }

}
