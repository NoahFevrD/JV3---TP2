using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [SerializeField] Settings settings;

    [Header("Animation")]
    [SerializeField] float fadeDuration;
    [SerializeField] float creditDuration;
    [Space(5)]

    [SerializeField] Animator mainMenuAnimator;
    
    [Header("Main Menu")]

    [SerializeField] CanvasGroup gameStartCanvas;
    [SerializeField] CanvasGroup[] gameStartCredit;
    [Space(10)]

    [Header("Options")]

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider voiceSlider;

    // -------------------------
    // Functions 
    // -------------------------

    void Start()
    {
        // Call Functions
        SetSettings();
        StartCoroutine("OnGameStart");
    }

    IEnumerator OnGameStart()
    {
        for(int i = 0;i < gameStartCredit.Length;i++)
        {
            // Fade In Credit
            gameStartCredit[i].DOFade(1, fadeDuration);
            yield return new WaitForSeconds(fadeDuration + creditDuration);

            // Fade Out Credit
            gameStartCredit[i].DOFade(0, fadeDuration);
            yield return new WaitForSeconds(fadeDuration);
        }

        // Fade Out Credit Canvas;
        gameStartCanvas.DOFade(0, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        mainMenuAnimator.SetTrigger("Open");
    }

    void SetSettings()
    {
        // Set Audio Settings
        masterSlider.value = settings.audioVolume.masterVolume;

        /*musicSlider.value = settings.audioVolume.musicVolume;
        sfxSlider.value = settings.audioVolume.sfxVolume;
        voiceSlider.value = settings.audioVolume.voiceVolume;*/
    }

    public void ChangeVolume()
    {
        // Apply Volume to Settings
        settings.audioVolume.masterVolume = masterSlider.value;

       /* settings.audioVolume.musicVolume = musicSlider.value;
        settings.audioVolume.sfxVolume = sfxSlider.value;
        settings.audioVolume.voiceVolume = voiceSlider.value;*/

        // Apply Volume to AudioMixer
        settings.audioMixer.SetFloat("masterVolume", settings.audioVolume.masterVolume);

        /*settings.audioMixer.SetFloat("musicVolume", settings.audioVolume.musicVolume);
        settings.audioMixer.SetFloat("sfxVolume", settings.audioVolume.sfxVolume);
        settings.audioMixer.SetFloat("voiceVolume", settings.audioVolume.voiceVolume);*/
    }

    // Fade In / Fade Out

    public void FadeIn(CanvasGroup canvasGroup)
    {
        // Fade In
        canvasGroup.DOFade(1, fadeDuration);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    public void FadeOut(CanvasGroup canvasGroup)
    {
        // Fade Out
        canvasGroup.DOFade(0, fadeDuration);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}
