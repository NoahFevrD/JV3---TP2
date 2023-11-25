using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Hitbox")]
    [SerializeField] AttackInfos attackInfos;

    // -------------------------
    // Functions
    // -------------------------

    void OnTriggerEnter(Collider other)
    {
        // Call SendDamageInfos if other has a HurtBox Script
        if(other.GetComponent<Hurtbox>() != null)
        other.GetComponent<Hurtbox>().SendDamageInfos(attackInfos);
    }
}
