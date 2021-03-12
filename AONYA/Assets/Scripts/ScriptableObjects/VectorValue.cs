using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Values/Vector Value", fileName = "New Vector Value")]
public class VectorValue : ScriptableObject {
    public Vector2 value;
    [SerializeField] private Vector2 defaultValue;

    private void OnEnable() {
        value = defaultValue;
    }
}
