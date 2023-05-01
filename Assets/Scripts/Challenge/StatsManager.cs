using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collects and saves the status of the current play through.
/// </summary>

public class Stats
{
    public string _phrase;
    public int _perfectCount = 0, _greatCount = 0, _missCount = 0, _score = 0;
    public HashSet<char> _missedChars;
}

public class StatsManager : MonoBehaviour
{
    Stats _stats = new Stats();

    private void Start()
    {
        _stats._missedChars = new HashSet<char>();
        _stats._phrase = GameManager.Instance.GetCurrentPhrase();
        EventManager.OnHandSignMatchedinChallenge += UpdateStats;
    }

    void UpdateStats(float Accuracy)
    {
        if (ChallengeManager.Instance.IsDetectedPowerUp() > 0) return; //if power up return
        if (Accuracy > 75) UpdateCounts(0); //for perfect
        else UpdateCounts(1); //for great
    }

    public void UpdateCounts(int type) // 0 - perfect, 1 - great, 2 - miss
    {
        switch (type)
        {
            case 0: _stats._perfectCount++; break;
            case 1: _stats._greatCount++; break;
            case 2: _stats._missCount++; break;
        }
    }

    public void missedCharacter(char ch) 
    {
        _stats._missedChars.Add(ch);
         UpdateCounts(2);
    }

    public Stats getStats() => _stats;

    public void SetScore(int score) => _stats._score = score;
    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinChallenge -= UpdateStats;
    }

}
