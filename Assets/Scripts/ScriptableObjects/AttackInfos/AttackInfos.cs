using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AttacKInfos_000", menuName ="Scriptable Object/AttackInfos")]

public class AttackInfos : ScriptableObject
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Attack")]
    public int damage;
    public float knockback;
    public float stunTime;
}