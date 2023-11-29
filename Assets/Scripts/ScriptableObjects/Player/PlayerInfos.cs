using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerInfos", menuName ="Scriptable Object/PlayerInfos")]

public class PlayerInfos : ScriptableObject
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Player's Info")]
    public int health;
    public int maxHealth;
    public int money;
    public float timeAlive;

    [Header("Level")]
    public int currentLevel = 1;
    public float experience;
    public float nextLevelExp;
    public float levelRatio;
    public int statsPoints;
    [Space(5)]

    public int levelCap;

    [Header("Bonus Stats")]
    public int defense;
    public int strength;
    public int speed;
    public int grind;

    // -------------------------
    // Functions
    // -------------------------

    // ~~~~~~~~~~~~~~~~~~
    // NEED TO BE REMOVED
    // ~~~~~~~~~~~~~~~~~~
    void Awake()
    {
        // RESET EVERYTHING
        currentLevel = 1;
        experience = 0;
        levelCap = 0;
        statsPoints = 0;
    }
    // ~~~~~~~~~~~~~~~~~~

    public void ExpGain(int expGain)
    {
        // Set Variables
        experience += expGain;

        if(experience >= nextLevelExp)
        LevelUp();
    }

    public void LevelUp()
    {
        if(currentLevel < levelCap)
        {
            // Set Variables
            currentLevel++;
            statsPoints++;

            experience -= nextLevelExp;
            nextLevelExp *= levelRatio;

            // Round Float
            MathF.Round(nextLevelExp);
        }
    }
}