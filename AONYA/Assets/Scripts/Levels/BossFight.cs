using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BossFight : MonoBehaviour {
    [Header("Player params")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject demon;

    [Header("Room params")]
    [SerializeField] private Room lightSide;
    [SerializeField] private Room darkSide;

    [Header("Light params")]
    [SerializeField] private float changeIntensity = 0.15f;
    [SerializeField] private float maxIntensity = 1.0f;
    [SerializeField] private float minIntensity = 0.0f;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private Light2D playerPointLight;
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    [SerializeField] private GameEvent demonHit;

    private bool changeGlobalLight;

    void Update() {
        RoomSwitch();
    }

    public void DestoryDarkness() {
        globalLight.intensity += changeIntensity;
        playerPointLight.intensity -= changeIntensity;

        if(globalLight.intensity >= 1.0f) {
            globalLight.intensity = maxIntensity;
        }

        if(playerPointLight.intensity <= 0.0f) {
            playerPointLight.intensity = minIntensity;
        }
    }

    private void RoomSwitch() {
        if (lightSide.playerInRoom) {
            playerPointLight.color = color1;
        } else if (darkSide.playerInRoom) {
            playerPointLight.color = color2;
        }
    }
}
