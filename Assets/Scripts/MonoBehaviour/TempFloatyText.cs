using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TempFloatyText : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("FloatyText")]
    [SerializeField] float upSpeed;
    [SerializeField] float duration;
    [SerializeField] float fadeOutDuration;
    CanvasGroup canvasGroup;

    // -------------------------
    // Functions
    // -------------------------

    private void Start()
    {
        // Set Variables
        canvasGroup = GetComponent<CanvasGroup>();

        // Call Functions
        Invoke("Destroy", duration);
        Invoke("FadeOut", duration - fadeOutDuration);
    }

    private void Update()
    {
        // Move GameObject
        transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
    }

    void FadeOut()
    {
        // Fade Out Canvas Group
        canvasGroup.DOFade(0, fadeOutDuration);
    }

    void Destroy()
    {
        // Destroy GameObject
        Destroy(gameObject);
    }
}
