using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the score of the Challenge mode.
/// </summary>

public class ScoreManager : MonoBehaviour
{
    TextMeshProUGUI _scoreText;
    int _score = 0;

    private void Start()
    {
        EventManager.OnHandSignMatchedinChallenge += UpdateScore;
        _scoreText = GetComponent<TextMeshProUGUI>();
        _scoreText.text = "Score: 0";
    }

    void UpdateScore(float Accuracy)
    {
        StartCoroutine(CheckScoreAfterOtherFunctionsHaveRun(Accuracy));
    }

    IEnumerator CheckScoreAfterOtherFunctionsHaveRun(float Accuracy)
    {
        yield return null; //wait for a second when all other functions for the same event have run
        if (ChallengeManager.Instance.IsDetectedPowerUp() > 0) yield break;

        if (Accuracy > 75) _score += (int)(20 + (ChallengeManager.Instance.GetCurrentStreak() * 0.1 * 20)); //perfect
        else _score += (int)(15 + (ChallengeManager.Instance.GetCurrentStreak() * 0.1 * 15)); //great

        if (ChallengeManager.Instance.GetIfDoublePoints()) _score *= 2; //if 2X power up is active

        _scoreText.text = "Score: " + _score.ToString();
    }

    public int GetScore() => _score;

    public void DecrementScore()
    {
        _score -= 20;
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinChallenge -= UpdateScore;
    }
}
