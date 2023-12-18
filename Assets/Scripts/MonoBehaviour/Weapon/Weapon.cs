using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.Services.Analytics;

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
    public WeaponInfos weaponInfos;
    [Space(5)]

    [SerializeField] Firearm firearm;
    [Space(5)]

    [SerializeField] LethalWeapon lethalWeapon;

    [Header("Grab Behaviour")]
    [SerializeField] bool ownedOnGrab;
    [SerializeField] UnityEvent onGrab;

    [Header("Shot Behaviour")]
    [SerializeField] float shootInterval;
    bool canShoot = true;

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

        // Lethal Weapon
        if(lethalWeapon.hitbox != null)
        {
            Hitbox hitbox = lethalWeapon.hitbox.GetComponent<Hitbox>();

            if(lethalWeapon.attackInfos != null)
            hitbox.attackInfos = lethalWeapon.attackInfos;

            if(lethalWeapon.contactParticle != null)
            hitbox.triggerParticle = lethalWeapon.contactParticle;

            if(audios.contact != null)
            hitbox.randomAudio = audios.contact;
        }
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
        if(firearm.bullet != null && isGrabbed && canShoot)
        {
            var bullet = Instantiate(firearm.bullet, firearm.weaponTip.position, firearm.weaponTip.rotation);
            bullet.GetComponent<Hitbox>().attackInfos = firearm.attackInfos;
            audios.fire.PlayRandomAudio();

            Invoke("CanShot", shootInterval);
        }
    }

    void CanShot()
    {
        canShoot = true;
    }

    // Grab Functions
    // -------------------------

    public void OnGrab()
    {
        // Set currentItem in Hand Script
        isGrabbed = true;
        onGrab.Invoke();

        if(ownedOnGrab)
        weaponInfos.owned = true;

        if(weaponInfos.owned)
        player.playerInfos.currentWeapon = weaponInfos;
        
        //audios.pickup.PlayRandomAudio();
    }

    public void OnDrop()
    {
        // Remove currentItem in Hand Script
        isGrabbed = false;
        //audios.drop.PlayRandomAudio();
    }
}