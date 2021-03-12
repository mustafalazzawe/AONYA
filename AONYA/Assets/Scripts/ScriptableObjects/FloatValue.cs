using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/Float Value", fileName = "New Float Value")]

// this script allows us to create an instance of a float and refer to it globally
public class FloatValue : ScriptableObject {
    // the value we will refer to globally
    public float value;

    // a default for our float value
    [SerializeField] private float defaultValue;

    private void OnEnable() {
        value = defaultValue;
    }
}
