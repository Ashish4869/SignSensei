using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSignCharacterController : MonoBehaviour
{ 
    string _phrase;
    int _currentCharacter = -1;
    List<GameObject> _characterImages = new List<GameObject>();

    private void Awake()
    {
        EventManager.OnHandSignMatchedinTraining += UpdateCharacters; //subscribing to event

        int i = 0;

        
        _phrase = GameManager.Instance.LoadTrainingPhraseList();
        

        foreach(Transform child in transform)
        {
            if (i == _phrase.Length - 1)
            {
                UpdateCharacters();
                return;
            }

            Image[] images = child.GetComponentsInChildren<Image>();
            Image NonHighImage = images[0];
            Image HighImage = images[1];

            NonHighImage.color = new Color(1, 1, 1, 1);
            HighImage.color = new Color(1, 1, 1, 0);

            _characterImages.Add(child.gameObject);

            child.gameObject.SetActive(true);

            if(_phrase[i] == ' ')
            {
                NonHighImage.enabled = false;
                HighImage.enabled = false;
                i++;
                continue;
            }

            string PathForNonHigh = "ImageData/TrainingAndChallenge/NonHighligtedLetter/" + _phrase[i];
            string PathForHigh = "ImageData/TrainingAndChallenge/HighligtedLetter/" + _phrase[i];
            var LoadedNonHighCharacterImage = Resources.Load<Sprite>(PathForNonHigh);
            var LoadedHighCharacterImage = Resources.Load<Sprite>(PathForHigh);
           
            NonHighImage.sprite = LoadedNonHighCharacterImage;
            HighImage.sprite = LoadedHighCharacterImage;
            i++;
        }
    }

  

    public void UpdateCharacters()
    {
        if (_currentCharacter == _phrase.Length - 2)
        {
            //Show end Screen
            if(GameManager.Instance.IsTrainingMode())
            {
                FindObjectOfType<HandSignAPI>().SetEvaulateStaus(false);
                FindObjectOfType<TrainingManager>().ShowEndScreen();
            }
            return;
        }
        if(_currentCharacter != -1)
        {
            //Set Animation
            Animator Anim = _characterImages[_currentCharacter].GetComponent<Animator>();
            Anim.SetTrigger("LetterFall");
        }

        _currentCharacter++;
        if (!_characterImages[_currentCharacter].GetComponentInChildren<Image>().IsActive()) _currentCharacter++;

        //Character to Process
        //set Anim
        Animator anim = _characterImages[_currentCharacter].GetComponent<Animator>();
        anim.SetTrigger("LetterRaise");
    }

    public string GetCharacterToPerform() => _phrase[_currentCharacter].ToString();

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinTraining -= UpdateCharacters;
    }
}
