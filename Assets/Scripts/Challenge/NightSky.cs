using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Assigns a hand sign for every image and plays the animation on loop
/// </summary>


public class NightSky : MonoBehaviour
{
    [Header("HandStars")]
    [SerializeField] Transform HandStars;

    
    private void Awake()
    {
        List<int> alphabets = new List<int>();

        for(int i = 1; i < 27; i++)
        {
            alphabets.Add(i);
        }

        foreach(Transform child in HandStars)
        {
            AssignLetter(child, alphabets);
        }
    }

    void AssignLetter(Transform child, List<int> alphabets)
    {
        //get a index corresponding Alphabet
        string alpha = Random.Range(0, alphabets.Count).ToString();
        int index = Int16.Parse(alpha);
        string path = "ImageData/NightSky/" + alphabets[index];

        //Load and set image sprite
        var LoadedSpriteData = Resources.Load<Sprite>(path);
        Image handSign = child.gameObject.GetComponent<Image>();
        handSign.sprite = LoadedSpriteData;
        float scale = Random.Range(0.4f, 0.6f);
        handSign.transform.localScale = new Vector2(scale, scale);
        handSign.SetNativeSize();

        //Remove elements from the list to prevent repeats
        alphabets.RemoveAt(index);

        //start animation at random times for each alphabet
        StartCoroutine(StartTwinkle(child, Random.Range(0, 3.0f)));
    }

    IEnumerator StartTwinkle(Transform child, float time)
    {
        Animator anim = child.gameObject.GetComponent<Animator>();
        yield return new WaitForSeconds(time);
        anim.SetTrigger("Twinkle");
    }
}
