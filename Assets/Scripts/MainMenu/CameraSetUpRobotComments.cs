using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetUpRobotComments : MonoBehaviour
{
    List<string> _animationTriggers = new List<string>()
    { 
        "CheckBG", "WellLitRoom", "CheatSheet",
        "DirectLightOnCamera", "FaceNotBehind", "GoodCamera", 
        "CameraRange" 
    };

    List<int> ranIndex = new List<int>();

    Animator _roboComments;
    private void Awake()
    {
        _roboComments = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(ShowNextHint());
    }

    IEnumerator ShowNextHint()
    {
        int randomNumber = 0;
        while (true)
        {
            if (ranIndex.Count == 0)
            {
                FillRanIndex();
            }

            randomNumber = GetRandomNumber();
            _roboComments.SetTrigger(_animationTriggers[randomNumber]);
            yield return new WaitForSeconds(5f);
        }
        
    }

    int GetRandomNumber()
    {
        int ran = Random.Range(0, ranIndex.Count);
        int ans = ranIndex[ran];
        ranIndex.RemoveAt(ran);
        return ans;
    }

    void FillRanIndex()
    {
        for (int i = 0; i < _animationTriggers.Count; i++)
        {
            ranIndex.Add(i);
        }
    }
}
