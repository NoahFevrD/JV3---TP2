using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("Input System")]
    public InputActionReference trigger;

    bool isGrabbed;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        if(trigger != null)
        trigger.action.performed += Shoot; 
    }

    void OnDisable()
    {
        if(trigger != null)
        trigger.action.performed -= Shoot; 
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // NEED TO BE CHANGE !!!
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        if(firearm.bullet != null && isGrabbed)
        {
            var bullet = Instantiate(firearm.bullet, firearm.weaponTip.position, firearm.weaponTip.rotation);
            bullet.GetComponent<Hitbox>().attackInfos = firearm.attackInfos;
            //audios.fire.PlayRandomAudio();

            Debug.Log("My broskiii");
        }

        Debug.Log("NAWR");
    }

    // Grab Functions
    // -------------------------

    public void OnGrab()
    {
        // Set currentItem in Hand Script
        isGrabbed = true;
        //audios.pickup.PlayRandomAudio();
    }

    public void OnDrop()
    {
        // Remove currentItem in Hand Script
        isGrabbed = false;
        //audios.drop.PlayRandomAudio();
    }
}