using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Detects whether the API is active
/// </summary>

public class CheckAPIStatus : MonoBehaviour
{
    private readonly string _url = "http://localhost:5000/status";
    float _delay = 1f;

    void Start()
    {
        StartCoroutine(GetAPIStatus());
    }

    IEnumerator GetAPIStatus()
    {
        UnityWebRequest APIStatus = UnityWebRequest.Get(_url);

        yield return APIStatus.SendWebRequest();

        if (APIStatus.result == UnityWebRequest.Result.ProtocolError) //checking if the API is called successfully
        {
            //give the API some breathing room and call again
            Invoke("GetAPIStatus", _delay);
        }

        JSONNode APIStatusJSON = JSON.Parse(APIStatus.downloadHandler.text);

        bool HandSignDetected = APIStatusJSON["is_running"];

        if(HandSignDetected)
        {
            FindObjectOfType<LoadingManager>().LoadMainGame();
        }

        Invoke("CallForAPIStatus", _delay); //call API again after a particular delay to prevent server overload.
    }

    void CallForAPIStatus()
    {
        StartCoroutine(GetAPIStatus());
    }
}
