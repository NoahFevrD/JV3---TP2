using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // -------------------------
    // Variable
    // -------------------------

    [Header("Bullet")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float duration;
    [Space(5)]

    [SerializeField] GameObject destroyParticle;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // Call Destroy after a certain duration
        Invoke("Destroy", duration);
    }

    void Update()
    {
        // Go Forward
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    public void Destroy()
    {
        // Destroy GameObject
        if(destroyParticle != null)
        Instantiate(destroyParticle, transform.position, transform.rotation);
        
        Destroy(gameObject);
    }
}
