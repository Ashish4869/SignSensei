using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip _clip;

    [Range(0f,1f)]
    public float _volume;

    public bool _isMusic;
    public bool _shouldLoop;

    [HideInInspector]
    public AudioSource _source;
}
