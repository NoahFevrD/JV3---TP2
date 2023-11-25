using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class Hurtbox : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Hurtbox")]
    [SerializeField] bool critical = false;
    [Space(5)]

    [SerializeField] PlayerController player;
    [SerializeField] PlayerController enemy;
    [SerializeField] PlayerController boss;

    public void SendDamageInfos(AttackInfos infos)
    {
        Debug.Log("Damage :" + infos.damage);
        Debug.Log("StunTime :" + infos.stunTime);
        Debug.Log("KnockBack:" + infos.knockback);

        // Player
        if(player != null)
        {}

        // Enemy
        else if(enemy != null)
        {}

        // Boss
        else if(boss != null)
        {}
    }
}
