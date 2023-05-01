using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the HandSign continously
/// </summary>

public class TVHandSignHint : MonoBehaviour
{
    Image _handSignHintImage;
    private void Awake()
    {
        _handSignHintImage = GetComponent<Image>();
    }

   public void UpdateCurrentHandSign()
   {
        string Path = "ImageData/TVImages/" + ChallengeManager.Instance.GetCurrentCharacterToPerformForChallenge();
        var LoadedSpriteData = Resources.Load<Sprite>(Path);
        _handSignHintImage.sprite = LoadedSpriteData;
        _handSignHintImage.SetNativeSize();
   }

   
}
