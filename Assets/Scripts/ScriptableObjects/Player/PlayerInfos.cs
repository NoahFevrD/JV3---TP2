using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerInfos", menuName ="Scriptable Object/PlayerInfos")]

public class PlayerInfos : ScriptableObject
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Start Position")]
    public Vector3 startPos;
    [HideInInspector]
    public bool teleportOnStart;

    [Header("Player's Info")]
    public int health;
    public float maxHealth;
    public int points;
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