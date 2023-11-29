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
        levelManager.LoadAsynchScene(sceneName);
    }
}
