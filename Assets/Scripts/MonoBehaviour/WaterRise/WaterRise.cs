using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRise : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Water Rise")]
    [SerializeField] WaterStat waterStat;
    Animator animator;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        if(GetComponent<Weapon>() == null)
        {
            if(waterStat.alreadylowered)
            animator.SetTrigger("Lowered");

            else if(!waterStat.alreadylowered && waterStat.lowerWater)
            {
                animator.SetTrigger("Lower");
                waterStat.alreadylowered = true;
            }
        }
    }

    public void OnGrab()
    {
        waterStat.lowerWater = true;
    }
}
