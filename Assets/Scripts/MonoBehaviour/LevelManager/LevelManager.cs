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

    [System.Serializable]
    public class SceneStartPosition
    {
        [Header("Start Position")]
        public Vector3 shipPos;
        public Vector3 shopPos;
        public Vector3 dungeonPos;
        public Vector3 bossPos;
    }

    // -------------------------
    // Variables 
    // -------------------------

    [Header("Scriptable Objects")]
    public PlayerInfos playerInfos;

    [Header("Level Manager")]
    public static LevelManager instance;
    [Space(5)]

    [SerializeField] int waitAfterLoad = 500;

    [Header("Loading Settings")]
    [SerializeField] GameObject loadingCanvas;
    [SerializeField] Slider loadingBar;
    [Space(5)]

    [SerializeField] float uiFadeDuration;

    [Header("Scene Properties")]
    [SerializeField] SceneStartPosition startPosition;

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

        Scene oldScene = SceneManager.GetActiveScene();

        do
        {
            await Task.Delay(waitAfterLoad);
            loadingBar.value = scene.progress;
        }

        while(scene.progress > 0.9f);

        // Load Next Scene
        scene.allowSceneActivation = true;
        loadingCanvas.GetComponent<CanvasGroup>().DOFade(0, uiFadeDuration);
        ChangePlayerPostion(oldScene);
    }

    void ChangePlayerPostion(Scene oldScene)
    {
        // Set Variables
        GameObject player = GameObject.Find("Player");
        Scene currentScene = SceneManager.GetActiveScene();
        playerInfos.teleportOnStart = false;
        playerInfos.weaponOnStart = true;

        if(currentScene.name == "SceneExterieure")
        {
            // Set Variables
            Vector3 startPos = Vector3.zero;
            playerInfos.weaponOnStart = true;

            if(oldScene.name == "MainMenu")
            startPos = startPosition.shipPos;

            if(oldScene.name == "SceneVaisseau")
            startPos = startPosition.shipPos;

            if(oldScene.name == "Fin")
            startPos = startPosition.shipPos;

            if(oldScene.name == "Magasin")
            startPos = startPosition.shopPos;

            if(oldScene.name == "SceneBoss")
            startPos = startPosition.bossPos;

            if(oldScene.name == "Dungeon")
            startPos = startPosition.dungeonPos;

            // Change Player Coords
            playerInfos.startPos = startPos;
            playerInfos.teleportOnStart = true;
        }

        if(currentScene.name == "SceneBoss" && currentScene.name == "Dungeon")
        playerInfos.weaponOnStart = true;
    }

    public void QuitGame()
    {
        Debug.Log($"So long my friend...");
        Application.Quit();
    }
}