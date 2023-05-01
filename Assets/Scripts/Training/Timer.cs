using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Counts the timer down and manages animation.
/// </summary>
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    
    int _timetoPerform = 10;
    float _timer;

    EventManager _eventManager;
    TrainingVisualFeedbackController _feedback;
    private void Awake()
    {
        _timer = _timetoPerform;
        _eventManager = FindObjectOfType<EventManager>();
        EventManager.OnHandSignMatchedinTraining += ResetTimer;
        _feedback = FindObjectOfType<TrainingVisualFeedbackController>();
    }

    private void Update()
    {
        if (_timer <= -1)
        {
            _feedback.ShowNegativeFeedback();
            _eventManager.OnHandSignMatchedInTrainingEvent();
            ResetTimer();
        }

        _timer -= Time.deltaTime;
        _timerText.text = Mathf.CeilToInt(_timer).ToString() + "s";
    }


    void ResetTimer()
    {
        _timer = _timetoPerform;
    }


    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinTraining -= ResetTimer;
    }
}
