using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 1.Shows the end screen;
/// </summary>

public class TrainingManager : MonoBehaviour
{
    
    [SerializeField] GameObject _trainingEndScreen;

   

    public void ShowEndScreen()
    {
        Debug.Log("Called");
        AudioManager.Instance.PlaySFX("WinTraining");
        FindObjectOfType<TrainingVisualFeedbackController>().enabled = false;
        FindObjectOfType<TimerHandle>().enabled = false;
        _trainingEndScreen.SetActive(true);
    }
    
}
