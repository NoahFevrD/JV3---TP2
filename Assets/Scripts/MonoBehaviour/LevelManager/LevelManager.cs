using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;
using Unity.Mathematics;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class SceneTransform
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    [System.Serializable]
    public class SceneStartPosition
    {
        [Header("Start Position")]
        public SceneTransform shipTransform;
        public SceneTransform shopTransform;
        public SceneTransform dungeonTransform;
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
        Scene oldScene = SceneManager.GetActiveScene();
        var scene = SceneManager.LoadSceneAsync(sceneName);

        // Loading
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
        loadingBar.value = 0;
        ChangePlayerPostion(oldScene, sceneName);
    }

    void ChangePlayerPostion(Scene oldScene, string sceneName)
    {
        // Set Variables
        GameObject player = GameObject.Find("Player");
        Scene currentScene = SceneManager.GetActiveScene();
        playerInfos.teleportOnStart = false;
        playerInfos.weaponOnStart = false;

        if(sceneName == "SceneExterieure")
        {
            // Set Variables
            Vector3 startPos = Vector3.zero;
            Quaternion startRotation = Quaternion.Euler(Vector3.zero);
            playerInfos.weaponOnStart = true;

            if(oldScene.name == "MainMenu")
            {
                startPos = startPosition.shipTransform.position;
                startRotation = startPosition.shipTransform.rotation;
            }

            if(oldScene.name == "SceneVaisseau")
            {
                startPos = startPosition.shipTransform.position;
                startRotation = startPosition.shipTransform.rotation;
            }

            if(oldScene.name == "Fin")
            {
                startPos = startPosition.shipTransform.position;
                startRotation = startPosition.shipTransform.rotation;
            }

            if(oldScene.name == "Magasin")
            {
                startPos = startPosition.shopTransform.position;
                startRotation = startPosition.shopTransform.rotation;
            }

            if(oldScene.name == "Dungeon")
            {
                startPos = startPosition.dungeonTransform.position;
                startRotation = startPosition.dungeonTransform.rotation;
            }

            // Change Player Coords
            playerInfos.startPos = startPos;
            playerInfos.startRotation = startRotation;
            playerInfos.teleportOnStart = true;
        }

        if(sceneName == "SceneBossFight" || sceneName == "Dungeon")
        playerInfos.weaponOnStart = true;
    }

    public void QuitGame()
    {
        Debug.Log($"So long my friend...");
        Application.Quit();
    }
}