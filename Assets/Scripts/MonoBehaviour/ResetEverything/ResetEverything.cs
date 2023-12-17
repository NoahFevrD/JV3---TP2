using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEverything : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Reset Everything")]
    [SerializeField] WeaponInfos[] weapons;
    [SerializeField] WeaponInfos defaultWeapon;
    [SerializeField] PlayerInfos playerInfos;
    [SerializeField] WaterStat waterStat;

    // -------------------------
    // Functions
    // -------------------------

    public void Reset()
    {
        // Weapons
        for(int i = 0;i < weapons.Length;i++)
        {
            weapons[i].owned = false;
        }

        playerInfos.currentWeapon = defaultWeapon;

        // Player Infos
        playerInfos.health = (int)playerInfos.defaultMaxHealth;
        playerInfos.maxHealth = playerInfos.defaultMaxHealth;

        playerInfos.points = 0;
        playerInfos.timeAlive = 0;

        playerInfos.currentLevel = 1;
        playerInfos.experience = 0;
        playerInfos.nextLevelExp = playerInfos.defaultNextLevelExp;

        playerInfos.defense = 0;
        playerInfos.strength = 0;
        playerInfos.speed = 0;
        playerInfos.grind = 0;

        // Water Stat
        waterStat.lowerWater = false;
        waterStat.alreadyLowered = false;
    }

    public void HealPlayer()
    {
        // Player Infos
        playerInfos.health = (int)playerInfos.maxHealth;
    }
}
