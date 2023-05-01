using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// Calls the backend and gets the detected handsign and its accuracy.
/// </summary>
public class HandSignAPI : MonoBehaviour
{
    HandSignCharacterController _handSignCharacterController;
    EventManager _eventManager;
    private readonly string _url = "http://localhost:5000/detect";
    float _delay = 0.4f;
    bool _canEvaluate = true;


    private void Awake()
    {
       _eventManager = FindObjectOfType<EventManager>();
       _handSignCharacterController = FindObjectOfType<HandSignCharacterController>();

        if (!GameManager.Instance.IsTrainingMode() && !GameManager.Instance.IsInCameraSetup()) _canEvaluate = false; //disable this component when in challenge mode only
    }

    private void OnEnable()
    {
        CheckForNextSign();
    }


    IEnumerator GetHandSign()
    {
        UnityWebRequest HandSign = UnityWebRequest.Get(_url);

        yield return HandSign.SendWebRequest();
       
        if (HandSign.result == UnityWebRequest.Result.ProtocolError) //checking if the API is called successfully
        {
            //give the API some breathing room and call again
            Invoke("CheckForNextSign", _delay);
        }

        JSONNode HandSignJSON = JSON.Parse(HandSign.downloadHandler.text);

        string HandSignDetected = HandSignJSON["detected_sign"];
        float Accuracy = HandSignJSON["accuracy"];
        Invoke("CheckForNextSign", _delay); //call API again after a particular delay to prevent server overload.
        GameManager.Instance.SetCameraSetUpValues(HandSignDetected, Accuracy);

        if (GameManager.Instance.IsInCameraSetup())
        {
            yield break;
        }


        if (!_canEvaluate) yield break;

        if (GameManager.Instance.IsTrainingMode())
        {
            if (_handSignCharacterController.GetCharacterToPerform() == HandSignDetected)
            {
                _eventManager.OnHandSignMatchedInTrainingEvent(); //call event when handsign matched in training
            }
        }
        else
        {
            if(ChallengeManager.Instance.GetCurrentCharacterToPerformForChallenge() == HandSignDetected)
            {
                _eventManager.OnHandSignMatchedInChallengeEvent(Accuracy); //call event when handsign matched in challenge
            }
        }
    }

    public void SetEvaulateStaus(bool status)
    {
        _canEvaluate = status;

        if (status == true) CheckForNextSign();
    }

    void CheckForNextSign()
    {
        if (!_canEvaluate || !gameObject.activeInHierarchy) return;
        StartCoroutine(GetHandSign());
    }
   
}

