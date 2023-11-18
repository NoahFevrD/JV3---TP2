using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] bool yes = false;

    private void Start() {
        if(yes) GameObject.Find("SoundManager").GetComponent<SoundManager>().StartCoroutine("StopAudio");
    }
    public void PlayAudio()
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager>().StartCoroutine("PlayAudio");
    }
}
