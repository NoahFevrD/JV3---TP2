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

    [HideInInspector]
    public PlayerController player;

    [Header("Arme")]
    public bool weaponOnStart;
    public WeaponInfos currentWeapon;

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
    public int experience;
    public int nextLevelExp;
    public float levelRatio;
    [Space(5)]

    public int levelCap;

    [Header("Bonus Stats")]
    public int healthBonus;
    [Space(5)]

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

            health += healthBonus;
            maxHealth += healthBonus;

            experience -= nextLevelExp;
            nextLevelExp =  (int)Mathf.Round( nextLevelExp * levelRatio);

            defense++;
            strength++;
            speed++;
            grind++;

            // Call Functions
            player.LevelUp();

            // Round Float
            MathF.Round(nextLevelExp);
        }
    }

    public int RoundDamage(int damage, int stat, float multiply)
    {
        // Round Given Damage
        float bonusDamage = stat * damage * multiply;
        int roundDamage = (int)MathF.Round(damage + bonusDamage);

        Debug.Log("Bonus Damage :" + bonusDamage);
        Debug.Log("Total Damage :" + roundDamage);

        return roundDamage;
    }
}