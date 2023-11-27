using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName ="Music_000",menuName ="Scriptable Object/Music")]

public class Music : ScriptableObject
{
    // -------------------------
    // Variables
    // -------------------------

    [Header("Music Parameter")]
    public string musicName;
    [Space(5)]
    
    public AudioClip musicIntro;
    public AudioClip musicLoop;
    [Space(5)]

    public bool loop;
}
