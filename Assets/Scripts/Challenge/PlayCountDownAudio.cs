using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays the audio for the countdown.
/// </summary>


public class PlayCountDownAudio : MonoBehaviour
{
    public void PlayCountDownSFX()
    {
        AudioManager.Instance.PlaySFX("CountDown");
    }
}
