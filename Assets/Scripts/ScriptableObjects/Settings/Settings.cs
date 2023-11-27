using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName ="Settings", menuName ="Scriptable Object/Settings")]

public class Settings : ScriptableObject
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class AudioVolume
    {
        [Range(-80, 0)]
        public float masterVolume;
        [Space(5)]

        [Range(-80, 0)]
        public float musicVolume;
        [Range(-80, 0)]
        public float sfxVolume;
        [Range(-80, 0)]
        public float voiceVolume;
    }

    // -------------------------
    // Variables
    // -------------------------

    [Header("Quality Seetings")]

    [Header("Audio Settings")]
    public AudioMixer audioMixer;
    public AudioVolume audioVolume;
}
