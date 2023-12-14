using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private SceneExtManager sceneManager;

    [SerializeField] private string nameCreator;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager.creator = nameCreator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
