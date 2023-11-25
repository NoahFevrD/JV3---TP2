using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class Firearm
    {
        [Header("Firearm")]
        public GameObject bullet;
        public AttackInfos attackInfos;
        [Space(5)]

        public float firerate;
        public Transform weaponTip;
    }

    [System.Serializable]
    public class LethalWeapon
    {
        [Header("Lethal Weapon")]
        public GameObject hitbox;
        public AttackInfos attackInfos;
        [Space(5)]

        public GameObject contactParticle;

    }

    [System.Serializable]
    public class Audios
    {
        public RandomAudio drop;
        public RandomAudio pickup;
        [Space(5)]

        public RandomAudio fire;
        public RandomAudio contact;
    }

    // -------------------------
    // Variables
    // -------------------------

    [Header("Weapon")]
    [SerializeField] WeaponInfos weaponInfos;
    [Space(5)]

    [SerializeField] Firearm firearm;
    [Space(5)]

    [SerializeField] LethalWeapon lethalWeapon;

    [Header("Components")]
    [SerializeField] Audios audios;
    Rigidbody rb;
    PlayerController player;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    public void EquipWeapon(bool right)
    {
        // Set Variables
        if(right)
        player.rightWeapon = gameObject;

        else
        player.leftWeapon = gameObject;
    }
}
