using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaterRise : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Water Rise")]
    [SerializeField] WaterStat waterStat;
    [SerializeField] UnityEvent onWaterLower;
    Animator animator;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        if(GetComponent<Weapon>() == null)
        {
            if(GetComponent<Animator>() != null)
            animator = GetComponent<Animator>();

            if(waterStat.alreadyLowered)
            {
                animator.SetTrigger("Lowered");
                onWaterLower.Invoke();
            }

            else if(!waterStat.alreadyLowered && waterStat.lowerWater)
            {
                animator.SetTrigger("Lower");
                waterStat.alreadyLowered = true;
                onWaterLower.Invoke();
            }
        }

        else
        {
            PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
            Weapon weapon = GetComponent<Weapon>();

            if(playerController.playerInfos.currentWeapon == weapon.weaponInfos.weaponPrefab)
            Destroy(gameObject);
        }
    }

    public void OnGrab()
    {
        waterStat.lowerWater = true;
    }
}
