using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WaterStat", menuName ="Scriptable Object/Water Stat")]
public class WaterStat : ScriptableObject
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Water Stat")]
    public bool lowerWater;
    public bool alreadylowered;
}
