using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton class that manages the variables of this game mode.
/// </summary>

public class ChallengeManager : MonoBehaviour
{
    //Singleton Pattern
    public static ChallengeManager _instance;
    public static ChallengeManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<ChallengeManager>();

                if(_instance == null)
                {
                    _instance = new GameObject().AddComponent<ChallengeManager>();
                }
            }

            return _instance;
        }
    }



    [SerializeField]Transform _parentGameObject;

    LettersInPhraseManager _lettersInPhraseManager;
    ChallengeHandSignCharacterController _challengeHandSignCharacterController;
    HandSignAPI _handSignAPI;
    DetectionMessageHandler _detectionMessageHandler;
    StatsManager _statsManager;
    GameObject _endScreen;
    GameObject _pause;
    GameObject _quitProgress;
    StreakHandler _streakHandler;
    ScoreManager _scoreManager;
    TVHandSignHint _tvHandSignHint;
    PowerUpUIManager _powerUpUIManager;


    bool _queueEmpty = false;
    bool _isdetected = false;
    bool _slowPowerUp = false, _doubleJeopardy = false, _battery = false;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        _challengeHandSignCharacterController = GetComponent<ChallengeHandSignCharacterController>();
        _lettersInPhraseManager = FindObjectOfType<LettersInPhraseManager>();
        _handSignAPI = FindObjectOfType<HandSignAPI>();
        _detectionMessageHandler = FindObjectOfType<DetectionMessageHandler>();
        _statsManager = FindObjectOfType<StatsManager>();
        _streakHandler = FindObjectOfType<StreakHandler>();
        _scoreManager = FindObjectOfType<ScoreManager>();
        _tvHandSignHint = FindObjectOfType<TVHandSignHint>();
        _powerUpUIManager = FindObjectOfType<PowerUpUIManager>();
        _endScreen = GameObject.Find("EndScreen");
        _endScreen.SetActive(false);
        _pause = GameObject.Find("Pause");
        _pause.SetActive(false);
        _quitProgress = GameObject.Find("QuitProgress");
        _quitProgress.SetActive(false);

    }

    public void QueueEmpty()
    {
        _queueEmpty = true;
    }
   
    IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(1f);
        if(_queueEmpty && IsAllChildrenEmpty())
        {
            //GetScore
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            int score = scoreManager.GetScore();
            _statsManager.SetScore(score);
            Stats stats = _statsManager.getStats();

            //Save HighScore
            GameManager.Instance.SetHighScore(score);

            //Display Stats
            _endScreen.SetActive(true);
            AudioManager.Instance.PlaySFX("WinChallenge");
            _endScreen.GetComponent<EndScreen>().SetEndScreen(stats);

            //Save Game data
            SaveSystem.SaveStat(stats);
        }
    }

    bool IsAllChildrenEmpty()
    {
        foreach(Transform child in _parentGameObject)
        {
            if (child.gameObject.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    public void CheckIfHandMissing()
    {
        if(GameManager.Instance.GetCurrentCharacterForCameraSetup() == "?")
        {
            //show MissUI
            _pause.SetActive(true);

            //play audio
            AudioManager.Instance.PlaySFX("Idle");

            //pause game
            Time.timeScale = 0;
        }
    }

    public void DeactivateFallingLetters()
    {
        foreach (Transform child in _parentGameObject)
        {
            child.gameObject.GetComponent<LetterController>().enabled = false;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pause.SetActive(false);
    }

    private void Update() //cheat keys for power up
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivatePowerUp(1, 100);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivatePowerUp(2, 100);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivatePowerUp(3, 100);
        }
    }
    public void ActivatePowerUp(int powerUpID, float Accuracy)
    {
        switch(powerUpID)
        {
            case 1:
                StartCoroutine(SlowDownPowerUp(Accuracy));
                break;

            case 2:
                StartCoroutine(DoubleJeopardy(Accuracy));
                break;

            case 3:
                StartCoroutine(BatteryPoweredTV(Accuracy));
                break;
        }
    }

    IEnumerator SlowDownPowerUp(float Accuracy)
    {
        //Activate power up
        _slowPowerUp = true;
        _powerUpUIManager.ActivatePowerUp(1);
        Time.timeScale = 0.5f;
        _lettersInPhraseManager.ActivateSnowFlake();
        AudioManager.Instance.PlaySFX("Slow");

        //set time and wait
        int ActiveTime = (Accuracy > 50) ? 10 : 5;
        yield return new WaitForSeconds(ActiveTime);

        //Deactivate power up
        _slowPowerUp = false;
        Time.timeScale = 1f;
        _powerUpUIManager.DeactivatePowerUp(1);
        _lettersInPhraseManager.DeactivateSnowFlake();
    }

    IEnumerator DoubleJeopardy(float Accuracy)
    {
        //Activate power up
        _doubleJeopardy = true;
        _powerUpUIManager.ActivatePowerUp(2);
        AudioManager.Instance.PlaySFX("2X");

        //Set time and wait
        int ActiveTime = (Accuracy > 50) ? 10 : 5;
        yield return new WaitForSeconds(ActiveTime);
        
        //Deactivate power up
        _doubleJeopardy = false;
        _powerUpUIManager.DeactivatePowerUp(2);
    }

    IEnumerator BatteryPoweredTV(float Accuracy)
    {
        //Activate power up
        _battery = true;
        _powerUpUIManager.ActivatePowerUp(3);
        AudioManager.Instance.PlaySFX("TV");

        //set time and wait
        int ActiveTime = (Accuracy > 50) ? 10 : 5;
        yield return new WaitForSeconds(ActiveTime);

        //Deactivate power up
        _battery = false;
        _powerUpUIManager.DeactivatePowerUp(3);
    }



    public string GetCurrentCharacterinPhrase() => _lettersInPhraseManager.GetCurrentCharacterinPhrase().ToUpper();

    public void Scan(bool condition) => _handSignAPI.SetEvaulateStaus(condition);

    public void CharacterDetected() => _isdetected = true;

    public bool GetIfCharacterDetected() => _isdetected;

    

    public void OnCharacterPassedThrough(string ch)
    {
        //Check if player hand is missing
        CheckIfHandMissing();

        //clarify if player couldnt perform handsign for the character
        if(GetIfCharacterDetected() == false)
        {
            _detectionMessageHandler.DisplayMissed();

            if(IsDetectedPowerUp() == 0)
            {
                if (_doubleJeopardy)
                {
                    _scoreManager.DecrementScore();
                }

                _streakHandler.ResetStreak();
                _statsManager.missedCharacter(ch[0]);
            }
            
        }

        StartCoroutine(CheckGameOver());
        _isdetected = false;
    }


    public void DequeCharacterFromFallingCharactersQueue()
    {
        //Dequeue
        _challengeHandSignCharacterController.DeQueue();

        //show currentCharacter
        _tvHandSignHint.UpdateCurrentHandSign();

    }

    public void QuitProgress()
    {
        Time.timeScale = 0f;
        _quitProgress.SetActive(true);
    }

    public void ResumeGameFromMainMenu()
    {
        Time.timeScale = 1f;
        _quitProgress.SetActive(false);
        _pause.SetActive(false);
    }

    public void EnqueueCharacterFromFallingCharacters(Tuple<char,int> t) => _challengeHandSignCharacterController.EnQueue(t);
    public string GetCurrentCharacterToPerformForChallenge() => _challengeHandSignCharacterController.GetCharacterToPerformFromFallingCharacter();
    
    public int IsDetectedPowerUp() => _challengeHandSignCharacterController.IsPowerUp();

    public int GetCurrentStreak() => _streakHandler.GetCurrentStreak();

    public bool GetIfDoublePoints() => _doubleJeopardy;
    
}
