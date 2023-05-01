using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Sets the text for intro
/// </summary>
public class SetPhraseForIntro : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _text.text = GameManager.Instance.GetCurrentPhrase();
    }

    
}
