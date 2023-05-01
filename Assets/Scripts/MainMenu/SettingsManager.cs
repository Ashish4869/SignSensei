using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the animations of the controls
/// </summary>


public class SettingsManager : MonoBehaviour
{
    [Header ("Control Animators")]
    [SerializeField] Animator _music;
    [SerializeField] Animator _sfx;

    [SerializeField] Animator _difficulty;
    [SerializeField] Animator _powerUps;

    [Header ("Other")]
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _settingNotification;

    bool _musicValue = true, _sfxValue = true, _powerUpValue = true;
    int _diffcultyValue = 0; 

    private void OnEnable()
    {
        SettingsData data = SaveSystem.LoadSettings();

        if(data != null)
        {
            _musicValue = data._music;
            _sfxValue = data._sfx;
            _diffcultyValue = data._difficulty;
            _powerUpValue = data._powerUps;
        }
        else
        {
            _musicValue = true; _sfxValue = true;
            _diffcultyValue = 0; 
        }

        TriggerTwoOptionControl(_music, _musicValue);
        TriggerTwoOptionControl(_sfx, _sfxValue);
        TriggerThreeOptionControl(_difficulty, _diffcultyValue);
        TriggerTwoOptionControl(_powerUps, _powerUpValue);
    }

    public void TriggerTwoOptionControl(Animator animatorControl, bool on)
    {
        if (on)
        {
            if (animatorControl.GetBool("On")) return;

            animatorControl.SetBool("On", true);
            animatorControl.SetBool("Off", false);
        }
        else
        {
            if (animatorControl.GetBool("Off")) return;

            animatorControl.SetBool("On", false);
            animatorControl.SetBool("Off", true);
        }
    }

    public void TriggerThreeOptionControl(Animator animatorControl, int diff)
    {
        if (diff == 0)
        {
            if (animatorControl.GetBool("E") == true) return;

            if (animatorControl.GetBool("M"))
            {
                animatorControl.SetBool("M", false);
            }

            if (animatorControl.GetBool("H"))
            {
                animatorControl.SetBool("H", false);
            }

            animatorControl.SetBool("E", true);
        }

        if (diff == 1)
        {
            if (animatorControl.GetBool("M") == true) return;

            if (animatorControl.GetBool("E"))
            {
                animatorControl.SetBool("E", false);
            }

            if (animatorControl.GetBool("H"))
            {
                animatorControl.SetBool("H", false);
            }

            animatorControl.SetBool("M", true);
        }

        if (diff == 2)
        {
            if (animatorControl.GetBool("H") == true) return;

            if (animatorControl.GetBool("M"))
            {
                animatorControl.SetBool("M", false);
            }

            if (animatorControl.GetBool("E"))
            {
                animatorControl.SetBool("E", false);
            }

            animatorControl.SetBool("H", true);
        }
    }

    public void TriggerMusicControlAnimation(bool on)
    {
        GameManager.Instance.SetMusicValue(on);
        TriggerTwoOptionControl(_music, on);
    }

    public void TriggerSFXControlAnimation(bool on)
    {
        GameManager.Instance.SetSFXValue(on);
        TriggerTwoOptionControl(_sfx, on);
    }

    public void TriggerPowerUpAnimation(bool on)
    {
        GameManager.Instance.SetPowerUpValue(on);
        TriggerTwoOptionControl(_powerUps, on);
    }

    public void TriggerDiffControlAnimation(int diff) // 0 - easy , 1 - meduim, 2- hard
    {
        GameManager.Instance.SetDifficultyValue(diff);
        TriggerThreeOptionControl(_difficulty, diff);
    }

   

    public void SaveSettings()
    {
        //save data into the file
        SaveSystem.SaveSettings(GameManager.Instance.GetSettings());
        AudioManager.Instance.ApplyMusicSettings();
        _settingNotification.SetActive(true);
    }

    public void CloseSettingOverlay()
    {
        _settingNotification.SetActive(false);
    }
}
