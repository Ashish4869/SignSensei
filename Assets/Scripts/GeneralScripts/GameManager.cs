using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This implements the singleton pattern.
/// </summary>
public class GameManager : MonoBehaviour
{
    bool _isTrainingMode, _isCameraSetup;
    string _currentPhrase,  _currentCharacter;
    float _accuracy;
    Settings _settings = new Settings();

    //Implementation of singleton Pattern
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if(_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //sets the settings at the start of the game
        SettingsData data = SaveSystem.LoadSettings();

        if (data != null)
        {
            _settings._music = data._music;
           _settings._sfx = data._sfx;
            _settings._difficulty = data._difficulty;
            _settings._powerUps = data._powerUps;
        }
    }

    public string LoadChallengePhraseList()
    {
        string path = "TextData/PhraseListChallenge";
        return GetPhrase(path);
    }

    public string LoadTrainingPhraseList()
    {
        string path = "TextData/PhraseListTraining";
        return GetPhrase(path);
    }
    

    public string GetPhrase(string path)
    {
        List<string> PhraseList = new List<string>();
        var loadedTextData = Resources.Load(path) as TextAsset; //getting the data from the text file
        string RawData = loadedTextData.text;
        string Phrase = "";

        foreach (char s in RawData)
        {
            if (s == '\n')
            {
                PhraseList.Add(Phrase);
                Phrase = "";
            }
            else
            {
                Phrase += s;
            }
        }

        return RandomPhraseFromList(PhraseList);
    }

    string RandomPhraseFromList(List<string> PhraseList)
    {
        int PhraseListSize = PhraseList.Count;
        int RandomNumber = Random.Range(0, PhraseListSize);
        _currentPhrase = PhraseList[RandomNumber];
        return _currentPhrase;
    }

    public void SetHighScore(int score) //saves the high score using player prefs
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
            return;
        }
        

        if (score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void PlayHoverMusic()
    {
        AudioManager.Instance.PlaySFX("ButtonHover");
    }
    public int GetHighScore() => PlayerPrefs.GetInt("HighScore");

    public void EnterCameraSetup(bool IsInCameraSetup) => _isCameraSetup = IsInCameraSetup;
    public bool IsInCameraSetup() => _isCameraSetup;
    public void SetGameMode(bool IsTraining) => _isTrainingMode = IsTraining;
    public bool IsTrainingMode() => _isTrainingMode;

    public string GetCurrentPhrase() => _currentPhrase;

    public void SetCameraSetUpValues(string CurrentCharacter, float Accuracy)
    {
        _currentCharacter = CurrentCharacter;
        _accuracy = Accuracy;
    }

    public string GetCurrentCharacterForCameraSetup() => _currentCharacter;
    public float GetCurrentAccuracyForCameraSetup() => _accuracy;

    //Settings
    public void SetMusicValue(bool condition) => _settings._music = condition;
    public void SetSFXValue(bool condition) => _settings._sfx = condition;
    public void SetDifficultyValue(int condition) => _settings._difficulty = condition;
    public void SetPowerUpValue(bool condition) => _settings.ChangePowerUpSetting(condition);

    //Music

    public bool GetMusicStatus() => _settings._music;
    public bool GetSFXStatus() => _settings._sfx;

    //settings
    public Settings GetSettings() => _settings;
    public int GetDifficultyMode() => _settings._difficulty;
    public bool GetPowerUps() => _settings._powerUps;

}
