using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public void ResumeTime()
    {
        Time.timeScale = 1;
    }

    public void PlayCloseDoor()
    {
        AudioManager.Instance.PlaySFX("TatamiClose");
    }

    public void PlayBrushStroke()
    {
        AudioManager.Instance.PlaySFX("BrushStroke");
    }

    public void PlayOpenDoor()
    {
        AudioManager.Instance.PlaySFX("TatamiOpen");
    }
}
