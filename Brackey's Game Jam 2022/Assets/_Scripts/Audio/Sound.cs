using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public enum AudioTypes { soundEffect, music}
    public AudioTypes audioType;

    [HideInInspector] public AudioSource source;
    public string clipName;
    public AudioClip audioClip;
    public bool isLoop;
    public bool playOnWake = false;

    [Range(0f, 1f)]
    public float volume = .75f;
}
