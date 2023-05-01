using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// converts the data present in the object into primitive variables
/// </summary>

[System.Serializable]
public class StatData 
{
    public string _phrase, _statID;
    public int _perfectCount = 0, _greatCount = 0, _missCount = 0, _score = 0;
    public char[] _missedChars;

    public StatData(Stats stat) 
    {
        _phrase = stat._phrase;
        _score = stat._score;
        _perfectCount = stat._perfectCount;
        _greatCount = stat._greatCount;
        _missCount = stat._missCount;

        int i = 0;

        _missedChars = new char[stat._missedChars.Count];
        foreach(char ch in stat._missedChars)
        {
            _missedChars[i] = ch;
            i++;
        }
    }

}
