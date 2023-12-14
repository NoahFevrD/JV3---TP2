using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Teleporter")]
    [SerializeField] string sceneName;
    [SerializeField] Vector3 position;

    LevelManager levelManager;

    // -------------------------
    // Functions
    // -------------------------

    void Start()
    {
        // Set Variables
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If other has XROrigin, Load Scene
        if(other.GetComponent<XROrigin>() != null)
        {
            // Set Variables
            other.GetComponent<PlayerController>().isInvincible = true;

            // Call Functions
            if(sceneName != null)
            levelManager.LoadAsynchScene(sceneName);

            else
            other.GetComponent<PlayerController>().TeleportPlayer(position);
        }
    }
}
