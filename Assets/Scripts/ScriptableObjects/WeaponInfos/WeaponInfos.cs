using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponInfos_000", menuName ="Scriptable Object/WeaponInfos")]

public class WeaponInfos : ScriptableObject
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Weapon's Infos")]
    public GameObject weaponPrefab;
    [Space(5)]

    public string weaponName;
    public bool owned = false;
    public int price;

    public int requiredFavors;
    public int discountPrice;
}
