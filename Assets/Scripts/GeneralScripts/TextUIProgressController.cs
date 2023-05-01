using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the UI for the progress text.
/// </summary>
public class TextUIProgressController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _phraseText;
    TrainingManager _trainingManager;

    int _currentCharacter = 0; //pointer that points to the current character to be performed.
    string _greenStart = "<color=green>";
    string _endColor = "</color>";
    string _grayStart = "<color=grey>";
    string _originalText; //Stores the original phrase pulled from game manager.

    private void Awake()
    {
        _trainingManager = FindObjectOfType<TrainingManager>();
        EventManager.OnHandSignMatchedinTraining += UpdatePhraseString; //subscribing to event
        _phraseText.text = GameManager.Instance.LoadTrainingPhraseList();
        _originalText = _phraseText.text;
        UpdatePhraseString();
    }

    public void UpdatePhraseString()
    {
        if (_currentCharacter >= _originalText.Length - 1) //reached end of phrase.
        {
            _phraseText.text = _grayStart + _originalText + _endColor;

            if(GameManager.Instance.IsTrainingMode()) _trainingManager.ShowEndScreen(); //show end screen if we are training mode
          
            return;
        }

        if (_originalText[_currentCharacter] == ' ') _currentCharacter++; // Skip space character.

        //do the operation only if we not at first character
        if (_currentCharacter > 0)
        {
            string Trainingtext = _originalText;
            string CompletedText = Trainingtext.Substring(0, _currentCharacter);
            string RemainingText = Trainingtext.Substring(_currentCharacter + 1);
            _phraseText.text = _grayStart + CompletedText + _endColor +
                                          _greenStart + Trainingtext[_currentCharacter] + _endColor +
                                          RemainingText;
        }
        else //if only first character needs to be green
        {
            _phraseText.text = _greenStart + _phraseText.text[_currentCharacter] + _endColor +
                                          _phraseText.text.Substring(1);
        }

        _currentCharacter++;
    }

    public string GetCurrentCharacterToPerform() => _originalText[index: _currentCharacter - 1].ToString();


    public void OnDestroy()
    {
        EventManager.OnHandSignMatchedinTraining -= UpdatePhraseString;
    }

    

}
