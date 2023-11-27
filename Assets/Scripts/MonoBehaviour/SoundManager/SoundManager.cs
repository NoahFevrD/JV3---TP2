using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // -------------------------
    // Variable
    // -------------------------

    [Header("Sound Manager")]
    public static SoundManager instance;

    [Header("Music")]
    public Music currentMusic;
    [SerializeField] float musicFadeDuration;
    [SerializeField] float timeBetweenMusic;

    [Header("Components")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // If instance already exist, destroy it
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        Destroy(gameObject);
    }

    public IEnumerator PlayAudio()
    {
        // If Music is Currently Playing, Fade to next Music
        if(musicSource.isPlaying)
        {
            musicSource.DOFade(0, musicFadeDuration);
            yield return new WaitForSeconds(musicFadeDuration + timeBetweenMusic);

            musicSource.Stop();
            musicSource.volume = 1;
        }

        if(currentMusic.loop)
        {
            musicSource.loop = true;
            //musicSource.PlayOneShot(currentMusic.musicIntro);

            Debug.Log("hello");

            musicSource.clip = currentMusic.musicLoop;
            musicSource.Play();
            musicSource.PlayScheduled(AudioSettings.dspTime + currentMusic.musicIntro.length);
        }

        else
        {
            musicSource.loop = false;
            musicSource.clip = currentMusic.musicIntro;
            musicSource.Play();
        }
    }

    public IEnumerator StopAudio()
    {
        musicSource.DOFade(0, musicFadeDuration);
        yield return new WaitForSeconds(musicFadeDuration);

        musicSource.Stop();
        musicSource.volume = 1;
    }
}
