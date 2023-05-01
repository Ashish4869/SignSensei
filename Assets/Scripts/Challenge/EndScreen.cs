using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shows the endscreen for challenge mode.
/// </summary>

public class EndScreen : MonoBehaviour
{
   
    TextMeshProUGUI _phrase, _score, _perfectCount, _greatCount, _MissCount, _LettersMissed;
    [SerializeField] Image _blueBotComment;

    private void Awake()
    { 
        //find all elements
        _phrase = GameObject.Find("PhraseEnd").GetComponent<TextMeshProUGUI>();
        _score = GameObject.Find("ScoreEnd").GetComponent<TextMeshProUGUI>();
        _perfectCount = GameObject.Find("PerfectCount").GetComponent<TextMeshProUGUI>();
        _greatCount = GameObject.Find("GreatCount").GetComponent<TextMeshProUGUI>();
        _MissCount = GameObject.Find("MissCount").GetComponent<TextMeshProUGUI>();
        _LettersMissed = GameObject.Find("LettersMissed").GetComponent<TextMeshProUGUI>();
    }

    public void SetEndScreen(Stats stats)
    {
        //set all values
        _phrase.text =  stats._phrase;
        _score.text = stats._score.ToString();
        _perfectCount.text = stats._perfectCount.ToString();
        _greatCount.text = stats._greatCount.ToString();
        _MissCount.text = stats._missCount.ToString();
        _LettersMissed.text = "";

        foreach(char ch in stats._missedChars)
        {
            _LettersMissed.text += ch + " ";
        }

        //as per score, choose a comment for the bot
        int totalPossibleScore = _phrase.text.Length * 20;
        string Path = "";

        if (stats._score > totalPossibleScore) //Good score Comments
        {
            int ran = Random.Range(0, 3);

            switch(ran)
            {
                case 0:
                    Path = "ImageData/Challenge/Unbeatable Score";
                    break;

                case 1:
                    Path = "ImageData/Challenge/A new high score";
                    break;

                case 2:
                    Path = "ImageData/Challenge/Damn too fast";
                    break;
            }
        }
        else if(stats._score < totalPossibleScore/2) //Average score Comments
        {
            int ran = Random.Range(0, 3);

           switch(ran)
           {
                case 0:
                    Path = "ImageData/Challenge/Go back to training";
                    break;

                case 1:
                    Path = "ImageData/Challenge/Practice More";
                    break;

                case 2:
                    Path = "ImageData/Challenge/Work On Your Skills";
                    break;
           }
        }
        else //poor score comments
        {
            int ran = Random.Range(0, 3);

            switch (ran)
            {
                case 0:
                    Path = "ImageData/Challenge/RoughAroundEdges";
                    break;

                case 1:
                    Path = "ImageData/Challenge/NothingTrainingCanFix";
                    break;

                case 2:
                    Path = "ImageData/Challenge/AlmostThere";
                    break;
            }
        }

        //Load and set the image sprite
        var LoadedSpriteData = Resources.Load<Sprite>(Path);
        _blueBotComment.sprite = LoadedSpriteData;
        _blueBotComment.SetNativeSize();
    }

}
