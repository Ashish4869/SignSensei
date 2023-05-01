using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Deals with Showing the HandSign Image that the player should perform for the current character (training).
/// </summary>

public class HandSignHintController : MonoBehaviour
{
    Image _handSignHintImage;
    HandSignCharacterController _handSignCharacterController;

    private void Start()
    {
        EventManager.OnHandSignMatchedinTraining += UpdateHandSignHint;
        _handSignCharacterController = FindObjectOfType<HandSignCharacterController>();
        _handSignHintImage = GetComponent<Image>();
        UpdateHandSignHint();
    }

    void UpdateHandSignHint()
    {
        string Path = "ImageData/Alphabets/" + _handSignCharacterController.GetCharacterToPerform();
        var LoadedSpriteData = Resources.Load<Sprite>(Path);
        _handSignHintImage.sprite = LoadedSpriteData;
        _handSignHintImage.SetNativeSize();
    }

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinTraining -= UpdateHandSignHint;
    }
}
