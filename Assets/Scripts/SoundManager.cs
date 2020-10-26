using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singletone<SoundManager>
{
    [System.Serializable]
    public struct Clip
    {
        public string name;
        public AudioClip audio;
    }

    public List<Clip> Clips = new List<Clip>();
    public AudioSource source;

    public void Play(string key)
    {
        var clip = Clips.Where(n => n.name == key).First();
        source.clip = clip.audio;
        source.Play();
    }
}
