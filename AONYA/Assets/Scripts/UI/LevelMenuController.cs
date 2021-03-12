using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class LevelMenuController : MonoBehaviour {
    [Header("General")]
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;

    // Update is called once per frame
    void Update() {
        // check if the user is pressing arrow key
        if (Input.GetAxisRaw("Vertical") != 0f) {
            // check if the key isnt currently down (pressed)
            if (!keyDown) {
                //check if the down arrow is being pressed
                if (Input.GetAxisRaw("Vertical") == -1) {
                    // check if the index is less than max 
                    // the curr selected btn is less than max num of btns
                    if (index < maxIndex) {
                        index++; // if not, increment (down list)
                    } else {
                        index = 1; // else, set to 1 (first btn)
                    }
                } else if (Input.GetAxisRaw("Vertical") == 1) {
                    // check if the index is greater than 1
                    if (index > 1) {
                        index--; // if not, decrement (up list)
                    } else {
                        index = maxIndex; // else, set to max (last btn)
                    }
                }
                keyDown = true;
            }
        } else {
            keyDown = false;
        }
    }
}