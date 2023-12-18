using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Hitbox")]
    public AttackInfos attackInfos;
    public GameObject triggerParticle;
    public RandomAudio randomAudio;

    // -------------------------
    // Functions
    // -------------------------

    void OnTriggerEnter(Collider other)
    {
        // Call SendDamageInfos if other has a HurtBox Script
        if(other.GetComponent<Hurtbox>() != null)
        {
            other.GetComponent<Hurtbox>().SendDamageInfos(attackInfos);

            if(triggerParticle != null)
            Instantiate(triggerParticle, transform.position, triggerParticle.transform.rotation);

            if(randomAudio != null)
            randomAudio.PlayRandomAudio();

            if(GetComponent<Bullet>() != null)
            GetComponent<Bullet>().Destroy();
        }
    }
}
