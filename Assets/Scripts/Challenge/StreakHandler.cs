using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the value of the streak.
/// </summary>
public class StreakHandler : MonoBehaviour
{
    int _streakCount = 0;
    TextMeshProUGUI _streakText;

    private void Awake()
    {
        EventManager.OnHandSignMatchedinChallenge += UpdateStreak;
        _streakText = GetComponent<TextMeshProUGUI>();
    }

    void UpdateStreak(float Accuracy)
    {
        if(ChallengeManager.Instance.IsDetectedPowerUp() > 0) return;

        _streakCount++;
        _streakText.text = _streakCount.ToString();
    }

    public void ResetStreak()
    {
        _streakCount = 0;
        _streakText.text =  _streakCount.ToString();
    }

    public int GetCurrentStreak() => _streakCount;

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinChallenge -= UpdateStreak;
    }


}
