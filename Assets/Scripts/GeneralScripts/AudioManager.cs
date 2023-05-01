using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioManager>();

                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<AudioManager>();
                }
            }

            return _instance;
        }
    }

    public static AudioManager _instance;

    private void Awake()
    {
        //Singleton pattern
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s._clip;
            s._source.volume = s._volume;
            s._source.loop = s._shouldLoop;
        }
    }

    public void PlaySFX(string name)
    {
        if (GameManager.Instance.GetSFXStatus() == false) return;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if (s == null)
        {
            Debug.Log("Sound:" + name + " not found!");
            return;
        }

        s._source.Play();
    }

   
    public void PlayMusic(string name, bool shouldPlay)
    {
        if (GameManager.Instance.GetMusicStatus() == false) return;

        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound:" + name + " not found!");
            return;
        }

        if(shouldPlay)
        {
            s._source.Play();
            StartCoroutine(FadeInMusic(s));
        }
        else
        {
            StartCoroutine(FadeOutMusic(s));
        }
        
    }
  

    IEnumerator FadeInMusic(Sound s)
    {
        s._source.volume = 0;
        while(s._source.volume < s._volume)
        {
            s._source.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        s._source.volume = s._volume;
    }

    IEnumerator FadeOutMusic(Sound s)
    {
        while (s._source.volume > 0)
        {
            s._source.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        s._source.Stop();
    }


    public void ApplyMusicSettings()
    {
        if (GameManager.Instance.GetMusicStatus() == false)
        {
            foreach(Sound s in sounds)
            {
                s._source.Stop();
            }
        }
        else
        {
            foreach (Sound s in sounds)
            {
                if (s.name == "MainMenu")
                {
                    PlayMusic("MainMenu",true);
                    return;
                }
            }
        }
    }
}
