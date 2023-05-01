using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the visual feeback for trainingMode
/// </summary>
public class TrainingVisualFeedbackController : MonoBehaviour
{
    Animator _feedbackAnimator;
    bool _shouldShowNegative = false;

    private void Awake()
    {
        _feedbackAnimator = GetComponent<Animator>();
        EventManager.OnHandSignMatchedinTraining += ShowPositiveFeedback;
    }

    void ShowPositiveFeedback()
    {
        if(_shouldShowNegative)
        {
            _shouldShowNegative = false;
            return;
        }

        _feedbackAnimator.SetTrigger("Correct");
        AudioManager.Instance.PlaySFX("Correct");
    }

    public void ShowNegativeFeedback()
    {
        _shouldShowNegative = true;
        _feedbackAnimator.SetTrigger("Wrong");
        AudioManager.Instance.PlaySFX("Wrong");
    }

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinTraining -= ShowPositiveFeedback;
    }
}
