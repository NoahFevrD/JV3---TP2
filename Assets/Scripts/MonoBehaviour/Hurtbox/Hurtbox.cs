using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    [SerializeField] EnemyController enemy;
    [SerializeField] PlayerController boss;

    [SerializeField] GameObject floatyText;

    void Start()
    {
        // Set Variables
        if(GetComponent<PlayerController>() != null)
        player = GetComponent<PlayerController>();

        if(GetComponent<EnemyController>() != null)
        enemy = GetComponent<EnemyController>();
    }

    public void SendDamageInfos(AttackInfos infos)
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // NEED TO BE CHANGE !!!
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if(floatyText != null)
        {
            var text = Instantiate(floatyText, transform.position + Vector3.up, transform.rotation);
            text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "- " + infos.damage;
        }

        // Player
        if(player != null)
        if(!player.isInvincible)
        player.TakeDamage(infos);

        // Enemy
        else if(enemy != null)
        enemy.TakeDamage(infos);

        // Boss
        else if(boss != null)
        {}
    }
}
