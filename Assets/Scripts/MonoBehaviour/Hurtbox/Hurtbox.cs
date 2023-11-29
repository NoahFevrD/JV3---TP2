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
    [SerializeField] PlayerController enemy;
    [SerializeField] PlayerController boss;

    [SerializeField] GameObject floatyText;

    public void SendDamageInfos(AttackInfos infos)
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // NEED TO BE CHANGE !!!
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        var text = Instantiate(floatyText, transform.position + Vector3.up, transform.rotation);
        text.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "- " + infos.damage.ToString();

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
