using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------


    // -------------------------
    // Variables 
    // -------------------------

    [Header("Level Manager")]
    public static LevelManager instance;
    [Space(5)]

    [SerializeField] int waitAfterLoad = 500;

    [Header("Loading Settings")]
    [SerializeField] GameObject loadingCanvas;
    [SerializeField] Slider loadingBar;
    [Space(5)]

    [SerializeField] float uiFadeDuration;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // If instance is already present, erase it
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        Destroy(gameObject);
    }

    public async void LoadAsynchScene(string sceneName)
    {
        // Set Variables
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        loadingCanvas.GetComponent<CanvasGroup>().DOFade(1, uiFadeDuration);

        do
        {
            await Task.Delay(waitAfterLoad);
            loadingBar.value = scene.progress;
        }

        while(scene.progress > 0.9f);

        // Load Next Scene
        scene.allowSceneActivation = true;
        loadingCanvas.GetComponent<CanvasGroup>().DOFade(0, uiFadeDuration);
    }

    public void QuitGame()
    {
        Debug.Log($"So long my friend...");
        Application.Quit();
    }
}
