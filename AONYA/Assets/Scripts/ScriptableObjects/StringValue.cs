using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/String Value", fileName = "New String Value")]

// this script allows us to create an instance of a string and refer to it globally
public class StringValue : ScriptableObject {
    // the value we will refer to globally
    public string value;
}
