using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Provide feedback to the player based on the text
/// </summary>

public class DetectionMessageHandler : MonoBehaviour
{
    TextMeshProUGUI _detectionMessage;
    float _timeForMessage = 0.75f;
    RobotFeedback[] _robotFeedback;
    ChallengeVisualFeeback _visualFeedback;

    private void Awake()
    {
        _visualFeedback = FindObjectOfType<ChallengeVisualFeeback>();
        EventManager.OnHandSignMatchedinChallenge += UpdateMessage;
        _detectionMessage = GetComponent<TextMeshProUGUI>();
        _robotFeedback = FindObjectsOfType<RobotFeedback>();
    }

    private void Start()
    {
        _detectionMessage.text = "";
    }


    void UpdateMessage(float Accuracy)
    {
        if (Accuracy > 75) //Perfect
        {
            StartCoroutine(DisplayMessage("PERFECT!!"));
            _robotFeedback[0].ShowChatBubble(0);
            _robotFeedback[1].ShowChatBubble(0);
        }
        else //Great
        {
            StartCoroutine(DisplayMessage("GREAT!!"));
            _robotFeedback[0].ShowChatBubble(1);
            _robotFeedback[1].ShowChatBubble(1);
        }

        ChallengeManager.Instance.Scan(false);
        _visualFeedback.ShowCorrectVisualFeedback();
        ChallengeManager.Instance.CharacterDetected();
        ChallengeManager.Instance.DequeCharacterFromFallingCharactersQueue();
    }

    IEnumerator DisplayMessage(string message)
    {
        _detectionMessage.text = message;
        yield return new WaitForSeconds(_timeForMessage);
        _detectionMessage.text = "";
    }

    public void DisplayMissed()
    {
        StartCoroutine(DisplayMessage("MISSED!!"));
        _robotFeedback[0].ShowChatBubble(2);
        _robotFeedback[1].ShowChatBubble(2);
        _visualFeedback.ShowWrongVisualFeedback();
        ChallengeManager.Instance.DequeCharacterFromFallingCharactersQueue();
    }

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinChallenge -= UpdateMessage;
    }
}
