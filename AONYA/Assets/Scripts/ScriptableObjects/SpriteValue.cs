using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/Sprite Value", fileName = "New Sprite Value")]

// this script allows us to create an instance of a sprite and refer to it globally,
// comes in handy for certain animations
public class SpriteValue : ScriptableObject {
    // the value we will refer to globally
    public Sprite value;
}
