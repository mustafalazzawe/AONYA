using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/Bool Value", fileName = "New Bool Value")]

// this script allows us to create an instance of a bool and refer to it globally,
// will also come in handy for setting a default "state" of a gameobject (isdoorlocked, chestclosed, etc)
public class BoolValue : ScriptableObject {
    // the value we will refer to globally
    public bool value;
    [SerializeField] private bool resetBool;

    private void OnEnable() {
        value = resetBool;
    }
}
