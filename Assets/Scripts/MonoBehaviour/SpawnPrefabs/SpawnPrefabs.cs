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
            var prefab = Instantiate(spawnsPrefab[i].prefab, spawnsPrefab[i].position, rotation);
            prefab.name = spawnsPrefab[i].prefab.name + i;
        }
    }

    public void SpawnOnlyIndex(int index)
    {
        if(index >= 0 && index < spawnsPrefab.Length)
        {
            Quaternion rotation = quaternion.Euler(spawnsPrefab[index].rotation);
            var prefab = Instantiate(spawnsPrefab[index].prefab, spawnsPrefab[index].position, rotation);
            prefab.name = spawnsPrefab[index].prefab.name;
        }

        else
        Debug.Log("Index is out of bounds");
    }
}
