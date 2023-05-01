using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the templates of all the events that will be called in the game.
/// </summary>
public class EventManager : MonoBehaviour
{
    //event called in the event that HandSign Matches with the character we expect in training Mode
    //Called from HandSignAPI
    //Classes affect - TextUIProgressController
    public delegate void HandSignMatchedinTraining();
    public static event HandSignMatchedinTraining OnHandSignMatchedinTraining;

    public void OnHandSignMatchedInTrainingEvent()
    {
        if(OnHandSignMatchedinTraining != null)
        {
            OnHandSignMatchedinTraining();
        }
    }

    //event called in the event that HandSign Matches with the character we expect in Challenge Mode
    //Called from HandsignAPI class
    // Classes affected - ChallengerManager, detectionMessageHandler
    public delegate void HandSignMatchedinChallenge(float accuracy);
    public static event HandSignMatchedinChallenge OnHandSignMatchedinChallenge;

    public void OnHandSignMatchedInChallengeEvent(float accuracy)
    {
        if (OnHandSignMatchedinChallenge != null)
        {
            OnHandSignMatchedinChallenge(accuracy);
        }
    }


}
