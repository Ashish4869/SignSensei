using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// This handles the falling of the letters either through a straight line or via some angle and collision.
/// </summary>

public class LetterController : MonoBehaviour
{
    [SerializeField] float _fallTime = 1f;
    float _straightFallTime = 3f, _angularFallTime = 1.5f;
    Image _fallingCharacterSprite;
    
    TextMeshProUGUI _letterText;

    bool _collisionPath;

    float _timeElasped;
    Vector3 _startPosition, _endPostion;

    private void Awake()
    {
        _fallingCharacterSprite = GetComponentInChildren<Image>();
        _letterText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        InitialiseLetter();
    }
    private void Update()
    {
        MaintainTrajectory();
        DisableIfOutofScreen();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("top")) ChallengeManager.Instance.Scan(true);
        else if (collision.gameObject.CompareTag("bottom")) ChallengeManager.Instance.Scan(false);
    }

    private void DisableIfOutofScreen()
    {
        if (transform.position.y < 0)
        {
            transform.position = new Vector3(0, Screen.height + 100, transform.position.z);
            ChallengeManager.Instance.OnCharacterPassedThrough(_letterText.text);
            gameObject.SetActive(false);
        }
    }

    private void MaintainTrajectory()
    {
        if (_timeElasped <= _fallTime) //linearly interpolating the letter
        {
            float t = _timeElasped / _fallTime;
            transform.position = Vector3.Lerp(_startPosition, _endPostion, t);
            _timeElasped += Time.deltaTime;
        }
        else if (_collisionPath) //resetting the values if it follows a collision path
        {
            _startPosition = _endPostion;

            float randomXPosition = Random.Range(Screen.width / 4, 3 * Screen.width / 4);

            _endPostion = new Vector3(randomXPosition, -50, transform.position.z);
            _timeElasped = 0;
        }
    }

    void InitialiseLetter()
    {
        int PowerUp = 0;
        string Path = "";
        string CurrentCharacter = ChallengeManager.Instance.GetCurrentCharacterinPhrase().ToUpper();
        if (CurrentCharacter == " ")
        {
            gameObject.SetActive(false);
            return;
        }

        if(CurrentCharacter == "*") //apply some power up, power up 1 - SLOW, power up 2 - 2X, power up 3 - TV
        {
            CurrentCharacter = (( (char)( (Random.Range(0, 26) + 'A') ) )).ToString();
            PowerUp = Random.Range(1, 4);

            if (PowerUp == 1) Path = "ImageData/PowerUps/1";
            else if (PowerUp == 2) Path = "ImageData/PowerUps/2";
            else if (PowerUp == 3) Path = "ImageData/PowerUps/3";
            else Path = "ImageData/PowerUps/0";
        }
        else
        {
            Path = "ImageData/PowerUps/0";
        }

        //load and set the bg for the falling character
        var LoadedSpriteData = Resources.Load<Sprite>(Path);
        _fallingCharacterSprite.sprite = LoadedSpriteData;
        _fallingCharacterSprite.SetNativeSize();

        _letterText.text = CurrentCharacter;
        _timeElasped = 0f;
        int ShouldBounce = Random.Range(0, 3);

        _collisionPath = ShouldBounce == 0 ? false : true;

        if (!_collisionPath) InitialiseLinearPositions();
        else InitialiseAngularPositions();
        Tuple<char,int> t = new Tuple<char, int>(CurrentCharacter[0], PowerUp);
        ChallengeManager.Instance.EnqueueCharacterFromFallingCharacters(t);

    }

    private void InitialiseAngularPositions()
    {
        _startPosition = new Vector3(Screen.width / 2, Screen.height + 50, transform.position.z); //start at the centre of the screen

        float randomYPosition = Random.Range(Screen.height / 3, 2 * Screen.height / 3);

        int left_right = Random.Range(0, 2); //returns 0 or 1
        int Xposition;
        if (left_right == 0) Xposition = 0;
        else Xposition = Screen.width;

        _endPostion = new Vector3(Xposition, randomYPosition, transform.position.z);
        _fallTime = _angularFallTime;
    }

    private void InitialiseLinearPositions()
    {
        float randomXPosition = Random.Range(Screen.width / 4, 3 * Screen.width / 4);  //later modify with sprites actual width and height
        _startPosition = new Vector3(randomXPosition, Screen.height + 50, transform.position.z);
        _endPostion = new Vector3(randomXPosition, -50, transform.position.z);
        _fallTime = _straightFallTime;
    }

    
}
