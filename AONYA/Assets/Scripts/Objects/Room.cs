using System;
using System.Collections;
using UnityEngine;

public class Room : MonoBehaviour {
    [Header("Room params")]
    [SerializeField] private StringValue playerTag;
    // [SerializeField] private string bossTag;
    [SerializeField] private GameObject roomCamera;

    [Header("Respawn params")]
    [SerializeField] private StringValue respawnTag;
    [SerializeField] private GameObject[] respawns;

    [NonSerialized] public bool playerInRoom;
    [NonSerialized] public bool bossInRoom;

    // executes once the other collider enters the room (enters the polygon collider) 
    private void OnTriggerEnter2D(Collider2D other) {
        // check if the player enters 
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            roomCamera.SetActive(true);
            RespawnObjects();

            playerInRoom = true;
        }

        // check if the boss enters
        // if (other.gameObject.CompareTag(bossTag) && !other.isTrigger) {
        //     bossInRoom = true;
        // }
    }

    // executes once the other collider exits the room (exits the polygon collider) 
    public void OnTriggerExit2D(Collider2D other) {
        // check if the player exits 
        if (other.gameObject.CompareTag(playerTag.value) && !other.isTrigger) {
            roomCamera.SetActive(false);
            DespawnObjects();

            playerInRoom = false;
        }

        // check if the oss exits
        // if (other.gameObject.CompareTag(bossTag) && !other.isTrigger) {
        //     bossInRoom = false;
        // }
    }

    // this function "respawns" the objects in the room with the tag respawnable, by setting them inactive
    void RespawnObjects() {
        foreach (GameObject respawn in respawns) {
            respawn.SetActive(true);
        }
    }

    // this function "despawns" the objects in the room with the tag respawnable, by setting them inactive
    void DespawnObjects() {
        foreach (GameObject respawn in respawns) {
            respawn.SetActive(false);
        }
    }
}
