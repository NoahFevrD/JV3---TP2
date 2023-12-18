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
    public class Audios
    {
        [Header("Player's Audio")]
        public RandomAudio damage;
        public RandomAudio death;
    }

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

    [HideInInspector]
    public bool isInvincible;
    bool dead = false;

    [Header("Player Controller")]
    public PlayerInfos playerInfos;
    [SerializeField] LevelManager levelManager;
    [SerializeField] Transform weaponSpawn;

    [Header("Components")]

    public ScreenUi screenUi;
    [SerializeField] Audios audios;

    // -------------------------
    // Functions
    // -------------------------

    // Start Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        playerInfos.player = this;

        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        // Call Functions
        TeleportOnStart();
        SetScreenUI();
    }

    void TeleportOnStart()
    {
        // Teleport Player if they are in the correct scene
        if(playerInfos.teleportOnStart)
        transform.position = playerInfos.startPos;

        if(playerInfos.weaponOnStart)
        {
            var prefab = Instantiate(playerInfos.currentWeapon.weaponPrefab, weaponSpawn.position, weaponSpawn.rotation);
            prefab.name = playerInfos.currentWeapon.name;
        }
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

    public void LevelUp()
    {
        Debug.Log("Level UP! Current Level : " + playerInfos.currentLevel);
    }

    public void MoneyUp(int amount)
    {
        float bonusMoney = playerInfos.grind * amount * .1f;
        playerInfos.points += (int)MathF.Round(amount + bonusMoney);

        Debug.Log("Bonus Money :" + bonusMoney);

        TextMeshProUGUI points = screenUi.points.GetComponent<TextMeshProUGUI>();
        points.text = playerInfos.points + " pts";
    }

    public void SetHealthUI()
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

    public void TakeDamage(AttackInfos infos)
    {
        // Set Variables
        playerInfos.health -= playerInfos.RoundDamage(infos.damage, playerInfos.defense, -.025f);

        if(playerInfos.health <= 0 && !dead)
        StartCoroutine("Death");

        // Play Animation & Audio
        else
        audios.damage.PlayRandomAudio();
        
        SetHealthUI();
    }

    IEnumerator Death()
    {
        // Set Variables
        dead = true;
        playerInfos.health = 0;

        audios.death.PlayRandomAudio();

        // Load Ship Scene
        yield return new WaitForSeconds(1.5f);
        levelManager.LoadAsynchScene("SceneVaisseau");
    }
}