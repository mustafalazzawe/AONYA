using System;
using UnityEngine;
using UnityEngine.Audio;

public class AnimatorFunctions : MonoBehaviour {
    [Header("General")]
    [SerializeField] MenuButton menuButton;

    [NonSerialized] public bool disableOnce;

    private AudioSource thisSource;
    private AudioMixerGroup thisMixer;

    void PlaySound(AudioClip sound) {
        // thisSource = menuButton.audioSource;
        // thisMixer = menuButton.mixerGroup;

        thisSource.clip = sound;
        thisSource.outputAudioMixerGroup = thisMixer;

        // prevent previous sound from playing 
        if (!disableOnce) {
            thisSource.Play();
        } else {
            disableOnce = false;
            thisSource.Stop();
        }
    }
}

