using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class ScreenUi
    {
        [Header("Screen UI")]
        public SceneExtManager sceneExtManager;
        [Space(10)]

        public GameObject healthBar;
        public Color defaultHealth;
        public Color criticHealth;
        [Space(5)]
        public GameObject nameplate;
        public GameObject points;

        public GameObject timer;
        [HideInInspector]
        public bool timerBool;
        [Space(10)]

        public string[] stopTimerScenes;
    }

    // -------------------------
    // Variables
    // -------------------------

    [Header("Player Controller")]
    [SerializeField] PlayerInfos playerInfos;
    [Space(5)]

    public GameObject rightWeapon;
    public GameObject leftWeapon;
    [Space(10)]

    public ScreenUi screenUi;

    // -------------------------
    // Functions
    // -------------------------

    // Start Functions
    // -------------------------

    void Start()
    {
        // Call Functions
        TeleportOnStart();
        SetScreenUI();
    }

    void TeleportOnStart()
    {
        // Teleport Player if they are in the correct scene
        if(playerInfos.teleportOnStart)
        transform.position = playerInfos.startPos;
    }

    void SetScreenUI()
    {
        // Set Variables
        // HealthBar
        SetHealthUI();

        // Points
        TextMeshProUGUI points = screenUi.points.GetComponent<TextMeshProUGUI>();
        points.text = playerInfos.points + " pts";

        // Nameplate
        TextMeshProUGUI sceneCreator = screenUi.nameplate.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        sceneCreator.text = "Fait par: " + screenUi.sceneExtManager.creator;

        StartCoroutine(UiFadeAnimation(screenUi.nameplate.GetComponent<CanvasGroup>()));

        // Timer
        SetTimer();
        screenUi.timerBool = true;
        for(int i = 0;i < screenUi.stopTimerScenes.Length;i++)
        {
            if(screenUi.stopTimerScenes[i] == SceneManager.GetActiveScene().name)
            screenUi.timerBool = false;
        }
    }

    // Update Functions
    // -------------------------

    void Update()
    {
        // Call Functions
        Timer();
    }

    void Timer()
    {
        if(screenUi.timerBool)
        {
            playerInfos.timeAlive += Time.deltaTime;
            SetTimer();
        }
    }

    void SetTimer()
    {
        // Convert Float to Seconds
        TextMeshProUGUI timer = screenUi.timer.GetComponent<TextMeshProUGUI>();
        
        TimeSpan ts = TimeSpan.FromSeconds(playerInfos.timeAlive);
        timer.text = string.Format("{0:0}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
    }

    IEnumerator UiFadeAnimation(CanvasGroup canvasGroup)
    {
        // Fade In
        canvasGroup.DOFade(1, .5f);

        yield return new WaitForSeconds(5.5f);

        // Fade Out
        canvasGroup.DOFade(0, .5f);
    }

    // Combat Functions
    // -------------------------

    void SetHealthUI()
    {
        // Set Variables
        Image healthbar = screenUi.healthBar.transform.GetChild(1).GetComponent<Image>();
        float healthRatio = playerInfos.health/playerInfos.maxHealth;

        // Fill
        healthbar.DOFillAmount(healthRatio, 1.5f);
        
        // Color
        if(healthRatio <= .25f)
        healthbar.color = screenUi.criticHealth;

        else
        healthbar.color = screenUi.defaultHealth;
    }
}