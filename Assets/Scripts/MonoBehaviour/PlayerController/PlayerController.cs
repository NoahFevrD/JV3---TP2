using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Player Controller")]
    [SerializeField] PlayerInfos playerInfos;
    [Space(5)]

    public GameObject rightWeapon;
    public GameObject leftWeapon;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // Call Functions
        TeleportOnStart();
    }

    void TeleportOnStart()
    {
        // Teleport Player if they are in the correct scene
        if(playerInfos.teleportOnStart)
        transform.position = playerInfos.startPos;
    }
}
