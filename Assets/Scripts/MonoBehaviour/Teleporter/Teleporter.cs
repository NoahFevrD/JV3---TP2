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
            // Call Functions
            if(sceneName != "")
            {
                other.GetComponent<PlayerController>().isInvincible = true;
                levelManager.LoadAsynchScene(sceneName);
                
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // If other has XROrigin, Teleport at Coords
        if(other.GetComponent<XROrigin>() != null)
        {
            if(sceneName == "")
            {
                other.transform.position = position;
                Debug.Log("Nuh uh");
            }
        }
    }
}
