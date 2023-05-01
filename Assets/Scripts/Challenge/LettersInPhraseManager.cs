using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages a prefabs for this module using Object Pooling and spawns them.
/// </summary>

public class LettersInPhraseManager : MonoBehaviour
{
    [SerializeField] GameObject _letterPrefab;
    [SerializeField] Transform _parentGameObject;

    float _spawnDelay = 2f;
    

    int _poolSize = 5;
    int _poolIndex = 0;
    List<GameObject> _letters;
    Queue<char> _characters = new Queue<char>();

   

    private void Start()
    {
        _letters = new List<GameObject>();
        SetDifficulty();
        PopulatePool();
        FillQueue();
        Invoke("SpawnAfterDelay", 10f);
    }

    private void SetDifficulty()
    {
        if(GameManager.Instance.GetDifficultyMode() == 0) //easy
        {
            _spawnDelay = 2f;
        }
        else if(GameManager.Instance.GetDifficultyMode() == 1) //meduim
        {
            _spawnDelay = 1.5f;
        }
        else //hard
        {
            _spawnDelay = 1f;
        }
    }

    void SpawnAfterDelay()
    {
        StartCoroutine(SpawnLetters());
    }

    void FillQueue()
    {
        string Phrase = GameManager.Instance.GetCurrentPhrase();
        List<int> _powerUpIndexes = new List<int>();
        _powerUpIndexes = SetPowerUpIndexes(Phrase, _powerUpIndexes);
        string s = "";

        int j = 0;
        for(int i = 0; i < Phrase.Length; i++)
        {
            if(i == _powerUpIndexes[j] && GameManager.Instance.GetPowerUps()) //put * in to the queue if settings applicable and if the index is same as iterator
            {
                s += '*';
                _characters.Enqueue('*');

                if(j != _powerUpIndexes.Count-1)
                {
                    j++;
                }
            }

            s += Phrase[i];
            _characters.Enqueue(Phrase[i]);
        }

       
    }

    private List<int> SetPowerUpIndexes(string phrase, List<int> list)
    {
        int NoOfPowerUps = phrase.Length / 5;
        int Range = phrase.Length / NoOfPowerUps;

        for(int i = 0; i < NoOfPowerUps; i++) //adds a power up in range expected
        {
            list.Add(UnityEngine.Random.Range(Range * i, Range * (i + 1) ));
        }

        return list;
    }

    void PopulatePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject Letter = Instantiate(_letterPrefab, _parentGameObject);
           _letters.Add(Letter);
           Letter.SetActive(false);
        }
    }

    IEnumerator SpawnLetters()
    {
        while(_characters.Count != 1) //to account for last space character
        {
            if (_poolIndex == _letters.Count) _poolIndex = 0; //resetting the iterator
            _letters[_poolIndex++].SetActive(true);
            yield return new WaitForSeconds(_spawnDelay);
        }
        ChallengeManager.Instance.QueueEmpty();
    }

    //Public Functions
    public string GetCurrentCharacterinPhrase() => _characters.Count == 0 ? " " : _characters.Dequeue().ToString(); //return character if queue is not empty

    public void ActivateSnowFlake()
    {
        foreach(GameObject g in _letters)
        {
            g.transform.Find("SnowFlake").gameObject.SetActive(true); ;
        }
    }

    public void DeactivateSnowFlake()
    {
        foreach (GameObject g in _letters)
        {
            g.transform.Find("SnowFlake").gameObject.SetActive(false); ;
        }
    }

}
