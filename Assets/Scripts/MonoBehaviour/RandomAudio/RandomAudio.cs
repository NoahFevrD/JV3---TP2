using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Random AudioClip")]
    [SerializeField] AudioClip[] audioClips;
    [Space(5)]

    [SerializeField] AudioSource audioSource;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        if(audioSource == null)
        audioSource = GetComponent<AudioSource>();
    }

    int RandomInt(int minInt, int maxInt)
    {
        int randomInt = Random.Range(minInt, maxInt);
        return randomInt;
    }

    public void PlayRandomAudio()
    {
        // Stop then Play a new Random AudioClip
        if(audioClips.Length > 0)
        {
            audioSource.Stop();
            audioSource.clip = audioClips[RandomInt(0, audioClips.Length)];
            audioSource.Play();
        }

        else
        Debug.Log("No Sound Assigned On RandomAuio :" + gameObject);
    }
}
