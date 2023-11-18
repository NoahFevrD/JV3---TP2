using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;

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
        // loadingCanvas.DOFade(1, uiFadeDuration);

        do
        {
            await Task.Delay(waitAfterLoad);
            // Slider.fillAmount = scene.progress;
        }

        while(scene.progress > 0.9f);

        // Load Next Scene
        scene.allowSceneActivation = true;
        // loadingCanvas.DOFade(0, uiFadeDuration);
    }

    public void QuitGame()
    {
        Debug.Log($"So long my friend...");
        Application.Quit();
    }
}
