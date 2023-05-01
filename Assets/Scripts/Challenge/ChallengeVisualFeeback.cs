using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows the feeback on the bottom bar by switching lights etc.
/// </summary>

public class ChallengeVisualFeeback : MonoBehaviour
{
    Animator _visualFeedback;
    private void Awake()
    {
        _visualFeedback = GetComponent<Animator>();
    }

    public void ShowCorrectVisualFeedback()
    {
        _visualFeedback.SetTrigger("Correct");
        AudioManager.Instance.PlaySFX("Perfect");
    }

    public void ShowWrongVisualFeedback()
    {
        _visualFeedback.SetTrigger("Wrong");
        AudioManager.Instance.PlaySFX("Missed");
    }
}
