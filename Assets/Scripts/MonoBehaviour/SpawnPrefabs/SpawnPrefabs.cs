using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnPrefabs : MonoBehaviour
{
    // -------------------------
    // Class
    // -------------------------

    [System.Serializable]
    public class Spawn
    {
        public GameObject prefab;
        public Vector3 position;
        public Vector3 rotation;
    }
    
    // -------------------------
    // Variables
    // -------------------------

    [Header("Spawn Prefabs")]
    [SerializeField] Spawn[] spawnsPrefab;

    // -------------------------
    // Functions
    // -------------------------

    public void SpawnAll()
    {
        for(int i = 0;i < spawnsPrefab.Length;i++)
        {
            Quaternion rotation = quaternion.Euler(spawnsPrefab[i].rotation);
            Instantiate(spawnsPrefab[i].prefab, spawnsPrefab[i].position, rotation);
        }
    }

    public void SpawnOnlyIndex(int index)
    {
        if(index >= 0 && index < spawnsPrefab.Length)
        {
            Quaternion rotation = quaternion.Euler(spawnsPrefab[index].rotation);
            Instantiate(spawnsPrefab[index].prefab, spawnsPrefab[index].position, rotation);
        }

        else
        Debug.Log("Index is out of bounds");
    }
}
