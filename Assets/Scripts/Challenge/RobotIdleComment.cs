using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows comment when the player is idle
/// </summary>


public class RobotIdleComment : MonoBehaviour
{
    private void OnEnable()
    {
        Image img = GetComponent<Image>();
        int ran = Random.Range(1, 3);

        //Set and load image sprite
        string Path = "ImageData/BotComment/" + ran.ToString();
        var LoadedSpriteData = Resources.Load<Sprite>(Path);
        img.sprite = LoadedSpriteData;
        img.SetNativeSize();

    }
}
