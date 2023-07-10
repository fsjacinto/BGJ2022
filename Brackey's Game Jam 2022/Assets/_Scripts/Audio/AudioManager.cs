using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixerGroup masterGroup;

    public AudioMixerGroup bgmGroup;
    public AudioMixerGroup sfxGroup;

    [SerializeField] private Sound[] sounds;

    protected override void Awake()
    {
        base.Awake();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;
            s.source.playOnAwake = s.playOnWake;

            switch (s.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = sfxGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = bgmGroup;
                    break;
            }

            if (s.playOnWake)
            {
                s.source.Play();
            }
        }
    }

    public void Play(string clipName)
    {
        Sound s = Array.Find(sounds, item => item.clipName == clipName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string clipName)
    {
        Sound s = Array.Find(sounds, item => item.clipName == clipName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public AudioSource GetSource(string sound)
    {
        Sound s = Array.Find(sounds, item => item.clipName == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }

        return s.source;
    }
}
