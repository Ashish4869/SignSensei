using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
/// Manages the StatUI elements.
/// </summary>

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] GameObject _statGroup;
    [SerializeField] GameObject _noStats;
    [SerializeField] GameObject _statOverlay;
    [SerializeField] GameObject _leftArrow;
    [SerializeField] GameObject _rightArrow;

    List<StatData> _statdata = new List<StatData>();

    int _statBatch = 0, _statSize = 0;

    private void Start()
    {
        _statSize = SaveSystem.GetNoOfStatFiles();

        SetUpStats();
        SetUpArrows();
    }

    private void SetUpArrows()
    {
        if ((_statBatch + 1) * 4 > _statSize) _rightArrow.SetActive(false);
        _leftArrow.SetActive(false);
    }

    void SetUpStats()
    {
        _statdata = SaveSystem.LoadStatData(_statBatch);

        if (_statdata == null)
        {
            _noStats.SetActive(true);
            _statGroup.SetActive(false);
        }
        else
        {
            _noStats.SetActive(false);
            _statGroup.SetActive(true);

            ConfigureStatOptions();
        }
    }

    private void ConfigureStatOptions()
    {
        //get all children
        int i = 0;
        foreach (Transform child in _statGroup.transform)
        {
            GameObject g = child.gameObject;
            g.SetActive(true);
            TextMeshProUGUI t = g.GetComponentInChildren<TextMeshProUGUI>();


            //assign text to each child
            t.text = GetStatName(i);
            //give the button a listener
            Button btn = g.GetComponentInChildren<Button>();
            btn.onClick.AddListener(delegate { ShowStat(t.text); });

            i++;

            if (i == _statdata.Count) break;
        }
    }

    string GetStatName(int i)
    {
        string statId = _statdata[i]._statID;
        string year = statId.Substring(0, 4);
        string month = statId.Substring(4, 2);
        string day = statId.Substring(6, 2);
        string hour = statId.Substring(8, 2);
        string min = statId.Substring(10, 2);
        string sec = statId.Substring(12, 2);
        string finalString = year + "/" + month + "/" + day + "    " + hour + ":" + min + ":" + sec; 
        return finalString;
    }


    public void ShowStat(string statid)
    {
        _statOverlay.SetActive(true);

        //Assign variables
        TextMeshProUGUI _statHeading,_phrase, _score, _perfectCount, _greatCount, _MissCount, _LettersMissed, _highScore;
        _highScore = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        _statHeading = GameObject.Find("StatHeading").GetComponent<TextMeshProUGUI>();
        _phrase = GameObject.Find("PhraseEnd").GetComponent<TextMeshProUGUI>();
        _score = GameObject.Find("ScoreEnd").GetComponent<TextMeshProUGUI>();
        _perfectCount = GameObject.Find("PerfectCount").GetComponent<TextMeshProUGUI>();
        _greatCount = GameObject.Find("GreatCount").GetComponent<TextMeshProUGUI>();
        _MissCount = GameObject.Find("MissCount").GetComponent<TextMeshProUGUI>();
        _LettersMissed = GameObject.Find("LettersMissed").GetComponent<TextMeshProUGUI>();

        //get the stat
        StatData stats = null;
        string heading = "";

        for (int i = 0; i < _statdata.Count; i++)
        {
            heading = GetStatName(i);
            if (heading == statid)
            {
                stats = _statdata[i];
                break;
            }
        }

        //set the stat values and display
        _highScore.text = GameManager.Instance.GetHighScore().ToString();

        string[] arr = heading.Split(' ');
        _statHeading.text = arr[0] + " at " + arr[arr.Length - 1];
        
        _phrase.text = stats._phrase;
        _score.text = stats._score.ToString();
        _perfectCount.text = stats._perfectCount.ToString();
        _greatCount.text = stats._greatCount.ToString();
        _MissCount.text = stats._missCount.ToString();
        _LettersMissed.text = "";

        foreach (char ch in stats._missedChars)
        {
            _LettersMissed.text += ch + " ";
        }
    }

    public void Close() => _statOverlay.SetActive(false);

    public void GetNext4Stats()
    {
        _statBatch++;
        ClearStats();
        SetUpStats();
        if ((_statBatch + 1) * 4 > _statSize) _rightArrow.SetActive(false);
        _leftArrow.SetActive(true);
    }

    private void ClearStats()
    {
        foreach (Transform child in _statGroup.transform)
        {
            GameObject g = child.gameObject;
            g.SetActive(false);
            TextMeshProUGUI t = g.GetComponentInChildren<TextMeshProUGUI>();
            t.text = "";
        }
    }

    public void GetPrev4Stats()
    {
        _statBatch--;
        ClearStats();
        SetUpStats();
        if (_statBatch == 0) _leftArrow.SetActive(false);
        _rightArrow.SetActive(true);
    }
}
