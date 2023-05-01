using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Takes what character to perfrom the falling characters themselves.
/// </summary>
public class ChallengeHandSignCharacterController : MonoBehaviour
{
    Queue<Tuple<char,int>> _charactersInSequence = new Queue<Tuple<char,int>>(); //stores the character and power up ID of the falling character.
    string _phrase;
    int _isPowerUp = 0;

    private void Awake()
    {
        _phrase = GameManager.Instance.LoadChallengePhraseList();
        EventManager.OnHandSignMatchedinChallenge += ApplyPowerUpIFApplicable;
    }

    void ApplyPowerUpIFApplicable(float Accuracy)
    {
        if(_isPowerUp > 0) //if the power Up ID is greater than zero then its a power up.
        {
            StartCoroutine(ActivatePowerUp(Accuracy));
        }
    }

    IEnumerator ActivatePowerUp(float Accuracy)
    {
        yield return new WaitForSeconds(0.2f); 
        ChallengeManager.Instance.ActivatePowerUp(_isPowerUp, Accuracy);
    }



    public string GetCharacterToPerformFromFallingCharacter()
    {
        if (_charactersInSequence.Count == 0) return "NoSignal"; //in the case that we cant get the next character, so return 'NoSignal' for the TV power up.
        return _charactersInSequence.Peek().Item1.ToString();
    }

    public void EnQueue(Tuple<char,int> t)
    {
        _charactersInSequence.Enqueue(t);
    }

    public void DeQueue() //remove the character from the queue and store whether it is a power up.
    {
        Tuple<char,int> t = _charactersInSequence.Dequeue();
        _isPowerUp = t.Item2;
    }

    public int IsPowerUp()
    {
        return _isPowerUp;
    }

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinChallenge -= ApplyPowerUpIFApplicable;
    }
}
